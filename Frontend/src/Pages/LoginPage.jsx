import { useState } from 'react'
import '../App.css'
import Navbar from '../Components/Navbar'



export default function LoginPage() {
    const [loginState, setLoginState] = useState(false)
    const [createUserState, setcreateUserState] = useState(false)

    const [name, setName] = useState("")
    const [age, setAge] = useState("")
    const [password, setPassword] = useState("")
    const [email, setEmail] = useState("")
    
    const CPCall = "CreatePerson"
    const CreatePerson = async () => {
        try{
            const response = await fetch(`/api/Person/CreatePerson`,{

                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    name:name,
                    Password:password,
                    Email:email,
                    age:age,
                }),
            })
        
        

        const data = await response.json()
        setBesked(`Oprettet: ${data.name}`)
        } catch (err) {
        setBesked(`Fejl: ${err.message}`)
        }
  
    } 

    function handleClick() {
        CreatePerson()
        setcreateUserState(!createUserState)

    }


    return (
        <>
        <Navbar/>
        {createUserState === false && 
        <div>
            <h1>Login</h1>
            <input type="text" placeholder="Brugernavn" value={name} onChange={(e) => setName(e.target.value)}/>
            <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)}/>
            <button onClick={() => setLoginState(true)}>Login</button>
            <button onClick={() => setcreateUserState(true)}>Lav bruger</button>

        </div>
        }
        {createUserState === true &&
        <div>
            <h1>Lav bruger</h1>
            <input type="text" placeholder="Brugernavn" value={name} onChange={(e) => setName(e.target.value)}/>
            <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)}/>
            <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)}/>
            <input type="number" placeholder="Age" value={age} onChange={(e) => setAge(e.target.value)}/>
            <button onClick={handleClick}>Opret</button>
            
        </div>
        }

        </>
    )
}

