import React, {useState} from "react"


export default function Activity ({activity, onDelete, onUpdate}){
    const [editing, setEditing] = useState(false);
    const [title, setTitle] = useState(activity.title); 

    const setActivityTitle = (value) => {
       setTitle(value);
    }

    const onBlurTitle = () => {
        setEditing(false);
        activity.title = title;

        if (onUpdate) onUpdate(activity);
    }

    const onEnterEditMode = () => {
        setEditing(true);
    }

    const onDeleteActivity = () => {
        if(onDelete)
        onDelete(activity);
    }

    const onDragActivity =  (event) => {
        event.dataTransfer.setData("activity.Id", activity.id);
    }

    return (
        <div draggable={!editing} className ={"activity"} onDragStart={onDragActivity}>
            <button className="btn-delete-activity" onClick={onDeleteActivity}>X</button>
            {editing ? (
                <input value={title} autoFocus onBlur={onBlurTitle} onChange={(event) => setActivityTitle(event.target.value)}/> 
            ) : (
                <span onDoubleClick={onEnterEditMode}>{activity.title}</span>
            )}
        </div>
    )

}