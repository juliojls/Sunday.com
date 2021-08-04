import React from "react"
import Activity from "../Activity";

export default function Pipe ({activities, status, onDelete, onUpdate, onActivityDrops }){

    const activitiesList = activities && activities.filter((a) => a.status === status);

    const onDeleteActivity = (activity) => {
        if(onDelete)
            onDelete(activity);
    }
    const onUpdateActivity =(activity) => {
        if(onUpdate)
            onUpdate(activity);
    }

    const ondragActivityOver = (event) => {
        event.preventDefault();
    }
     const onDropActivity = (event) => {
        const activityId = event.dataTransfer.getData("activity.Id");
        if(activityId && onActivityDrops)
            onActivityDrops(activityId);
     }

    let title = "Concluido";

    if (status === 0)
        title = "Aguardando";
    else if (status === 1)
        title = "Em andamento";

    return (
        <div className ={`pipe pipe-${status}`} onDragOver={ondragActivityOver} onDrop={onDropActivity}>
            <span className="pipe-title">
                {title} / {activitiesList.length}
            </span>
            {
                activitiesList.map((activity, index) => {
                    return <Activity activity = {activity} key={index} onDelete={onDeleteActivity} onUpdate={onUpdateActivity}/>

                })
            }
        </div>
    )

}