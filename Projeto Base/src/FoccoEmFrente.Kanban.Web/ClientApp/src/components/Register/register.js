import { post } from "jquery";
import React, {useState} from "react";
import HttpRequest from "../../Utils/HttpRequest";

export default function Register({history}) {
   const [email, setEmail] = useState('');
   const [password, setPassword] = useState('');
   const [confirmPassword, setConfirmPassword] = useState('');

   const onRegister = async (event) => {
      event.preventDefault();

      const response = await new HttpRequest("account/register","POST")
         .setBody({
            email : email,
            Password : password,
            confirmPassword: confirmPassword
         })
         .send();
            
      if(!response.ok )
      {
         window.alert(response.errorMessage);
         return
      }

      localStorage.setItem("tokem", response.data);
      history.push("/");
   };

   const onVoltar = () => {
      history.push("/login");
   }


   return (
      <div className = "content" width={450}>
         <p>Crie uma conta no <strong>Sunday.com</strong> </p>
         <form onSubmit={onRegister}>
            <label htmlFor="email">E-mail</label>
            <input id="email" type="email" placeholder="E-mail" value={email} onChange={(event) => setEmail(event.target.value)} />
            <label htmlFor="senha">Senha</label>
            <input id="password" type="password" placeholder="Informe sua Senha" value={password} onChange={(event) => setPassword(event.target.value)}/>
            <label htmlFor="confirm-password"> Confirmar Senha</label>
            <input id="confirm-Password" type="password" placeholder="Confirme sua Senha" value={confirmPassword} onChange={(event) => setConfirmPassword(event.target.value)}/>
            <button className = "btn btn-primary" type ="submit">Registrar</button>
            <button className = "btn btn-secondary" onClick={onVoltar}>voltar</button>
         </form>
      </div>
   );
}