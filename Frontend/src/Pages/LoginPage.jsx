import '../App.css'
import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import Navbar from '../Components/Navbar'
import { useAuth } from '../Components/Authcontext.jsx'

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
   <>
    <Navbar />

    {createUserState === false &&
    <div>
      <h1>Login</h1>
      <input id="inputContent" type="text" value={username} placeholder="Username" onChange={(e) => setUsername(e.target.value)}/>
      <input id="inputContent" type="password" value={password} placeholder="Password" onChange={(e) => setPassword(e.target.value)}/>
    <input id="inputContent" type="email" value={email} placeholder="Email" onChange={(e) => setEmail(e.target.value)}/>
      <button id="CommonButton" onClick={handleSend}>Login</button>
      <button id="CommonButton" onClick={handleClick}>Create User</button>
    </div>
    }
    {createUserState === true &&
    <div>
      <h1>Create User</h1>
        <input id="inputContent" type="text" value={username} placeholder="Username" onChange={(e) => setUsername(e.target.value)}/>
        <input id="inputContent" type="email" value={email} placeholder="Email" onChange={(e) => setEmail(e.target.value)}/>
        <input id="inputContent" type="password" value={password} placeholder="Password" onChange={(e) => setPassword(e.target.value)}/>
        <input id="inputContent" type="number" value={age} placeholder="Age" onChange={(e) => setAge(e.target.value)}/>
        <button id="CommonButton" onClick={handleSend}>Create User</button>
    </div>
    }

    {besked && <p>{besked}</p>}
   </>
  )
}