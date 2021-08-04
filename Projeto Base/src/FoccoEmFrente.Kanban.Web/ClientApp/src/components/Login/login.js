import React, {useState} from "react";
import HttpRequest from "../../Utils/HttpRequest";

export default function Login({history}) {
   const [FormLogin, setFormLogin] = useState({email:"", password:""});

   const setEmail =(event) => {
      setFormLogin({...FormLogin, email: event.target.value});
   }

   const setPassword =(event) => {
      setFormLogin({...FormLogin, password: event.target.value});
   }

   const onLogin = async (event) => {
      event.preventDefault();

      const response = await new HttpRequest("account/login","POST")
         .setBody(FormLogin)
         .send();

      if(!response.ok )
      {
         window.alert(response.errorMessage);
         return
      }

      localStorage.setItem("tokem", response.data);
      history.push("/");
   };

   const onRegister = () => {
      history.push("/register");
   };

   return (
      <div className = "content" width={450}>
         <p>Bem vindo ao <strong>Sunday.com</strong>, o melhor sistema para gestão de tarefas.</p>
         <form onSubmit={onLogin}>
            <label htmlFor="email">E-mail</label>
            <input id="email" type="email" placeholder="E-mail" value ={FormLogin.email} onChange={setEmail}/>
            <label htmlFor="senha">Senha</label>
            <input id="senha" type="password" placeholder="Informe sua Senha" value ={FormLogin.password} onChange={setPassword}/>
            <button className = "btn btn-primary" type ="submit">Entrar</button>
            <button className = "btn btn-secondary" onClick={onRegister}>Registrar</button>
         </form>
      </div>
   );   
   
}