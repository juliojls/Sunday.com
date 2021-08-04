import React, {useState, useEffect} from "react";
import Pipe from "./Pipe";
import './home.css';
import HttpRequest from "../../Utils/HttpRequest";
import Activity from "./Activity";

export default function Home({history}) {
   const [activities, setActivities] = useState([]);

   const token = localStorage.getItem("tokem");
   if(!token)
      history.push("/login");

   const loadActivities = async () => {
      
      const response = await new HttpRequest("activties","GET")
      .setTokem(token)
      .send();

      if(!response.ok)
         {
            window.alert(["Não foi possivel buscar as tarefas", response.errorMessage]);
            return;
         }

      setActivities(response.data);
   };  
   
   const addActivity = async () => {
     const activity =  {
         Title:"Nova atividade",
         Status: 0,
      };

      const response = await new HttpRequest("activties","POST")
      .setBody(activity)
      .setTokem(token)
      .send();

      if(!response.ok)
      {
         window.alert(["Não foi possivel inserir a tarefa", response.errorMessage]);
         return;
      }

      setActivities([...activities, response.data]);
   }

   const updateActivityStatus = async (activityId, status) => {
         const activity = activities.find(a =>a.id === activityId);

         const action = status === 0 ? "todo" : status === 1 ? "doing" : "done";

         const response = await new HttpRequest(`activties/${activityId}/${action}`,"PUT")
         .setTokem(token)
         .send();

         if(!response.ok){
            window.alert(["Não foi possivel atualizar o status da tarefa", response.errorMessage]);
            return;
         }

         await loadActivities();

   }

   const deleteActivity = async (activity) => {
      const response = await new HttpRequest(`activties/${activity.id}`,"DELETE")
      .setTokem(token)
      .send();

      if(!response.ok)
      {
         window.alert(["Não foi possivel excluir a tarefa", response.errorMessage]);
         return;
      }

      setActivities(activities.filter(a =>a.id !== activity.id));
   }

   const updateActivity = async (activity) => {
      const response = await new HttpRequest("activties","PUT")
      .setTokem(token)
      .setBody(activity)
      .send();

      if(!response.ok)
      {
         window.alert(["Não foi possivel atualizar a tarefa", response.errorMessage]);
         await loadActivities();
         return;
      }
   }


   const onExit = () => {
      localStorage.removeItem("tokem");
      history.push("/login");
   };

   useEffect(() =>{
      loadActivities();
   },[]);


   return (
      <div className = "content" width={800}>
         <p>Bem vindo ao <strong>Sunday.com</strong>.</p>
         <p>Esse é seu canvas para organizar suas atividades. Crie novas atividades e mantenaha elas sempre atualizadas</p>
            <div className = "canvas">
               <Pipe activities={activities} status={0} onDelete={deleteActivity} onUpdate={updateActivity} onActivityDrops={(activityId) => updateActivityStatus(activityId, 0)} />
               <Pipe activities={activities} status={1} onDelete={deleteActivity} onUpdate={updateActivity} onActivityDrops={(activityId) => updateActivityStatus(activityId, 1)}/>
               <Pipe activities={activities} status={2} onDelete={deleteActivity} onUpdate={updateActivity} onActivityDrops={(activityId) => updateActivityStatus(activityId, 2)}/>
            </div>
         <button className = "btn btn-primary" onClick ={addActivity}>Adiconar Atividades</button>
         <button className = "btn btn-secondary" onClick={onExit}>Sair</button>
      </div>
   );
}
