import '../App.css'
import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import Navbar from '../Components/Navbar'
import { useAuth } from '../Components/Authcontext.jsx'


// States til username osv.
export default function Login() {
  const navigate = useNavigate()
  const { send } = useAuth()
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [email, setEmail] = useState('')
  const [age, setAge] = useState('')
  const [createUserState, setcreateUserState] = useState(false)
  const [besked, setBesked] = useState('')

  // Samme funktion til login og opret - kun endpoint og data er forskellig
  // Tjekker om username og password er udfyldt, hvis ikke afbrydes handlingen.
  // Funktionen udnytter genbrugelig logik. tjekker createuserstate, hvis true kaldes register, med mail og age, hvis false kaldes login, med username og password
  // Hvis den fejler navigere den til forsiden
  // handleclick skifter createuserstate fra true til false, eller omvendt, så brugeren kan skifte visning.
  async function handleSend() {
    if (!username || !password) return setBesked('Udfyld brugernavn og password')
    try {
      const fejl = createUserState
        ? await send('Register', { username, password, email: email || null, age: Number(age) || 0 })
        : await send('Login', { username, password })
      if (fejl) setBesked(fejl)
      else navigate('/') // logget ind -> forsiden
    } catch (err) {
      setBesked(`Fejl: ${err.message}`)
    }
  }

  function handleClick() {
    setcreateUserState(!createUserState)
  }

  return (
    // I return er der gjort brug af betinget rendering, så hvis nu er createuserstate er false skal den vise det ene og hvis true det andet
   <>
    <Navbar />

    <main className="PageShell LoginContent">
      {createUserState === false &&
      <section className="LoginPanel">
        <h1>Login</h1>
        <input aria-label="Brugernavn" type="text" value={username} placeholder="Brugernavn" onChange={(e) => setUsername(e.target.value)}/>
        <input aria-label="Adgangskode" type="password" value={password} placeholder="Adgangskode" onChange={(e) => setPassword(e.target.value)}/>
        <input aria-label="Email" type="email" value={email} placeholder="Email" onChange={(e) => setEmail(e.target.value)}/>
        <div className="LoginActions">
          <button id="CommonButton" onClick={handleSend}>Login</button>
          <button id="CommonButton" onClick={handleClick}>Opret bruger</button>
        </div>
      </section>
      }
      {createUserState === true &&
      <section className="LoginPanel">
        <h1>Opret bruger</h1>
        <input aria-label="Brugernavn" type="text" value={username} placeholder="Brugernavn" onChange={(e) => setUsername(e.target.value)}/>
        <input aria-label="Email" type="email" value={email} placeholder="Email" onChange={(e) => setEmail(e.target.value)}/>
        <input aria-label="Adgangskode" type="password" value={password} placeholder="Adgangskode" onChange={(e) => setPassword(e.target.value)}/>
        <input aria-label="Alder" type="number" value={age} placeholder="Alder" onChange={(e) => setAge(e.target.value)}/>
        <div className="LoginActions">
          <button id="CommonButton" onClick={handleSend}>Opret bruger</button>
          <button id="CommonButton" onClick={handleClick}>Tilbage til login</button>
        </div>
      </section>
      }
      {besked && <p className="FormMessage" role="alert">{besked}</p>}
    </main>
   </>
  )
}