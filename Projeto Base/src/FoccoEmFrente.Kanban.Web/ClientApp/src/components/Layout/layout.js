import React from "react";
import Logo from '../../assets/logo.png'

export default function Layout(props) {
   return (
      <div className = "form-container">
         <img src ={Logo} alt="Sunday"/>
         <div className = "Content">
            {props.children}
         </div>   
      </div>
   )
}
