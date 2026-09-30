import '../App.css'
import { useNavigate } from 'react-router-dom'
import { useAuth } from './Authcontext.jsx'

// denne fil indeholder alt der skal være i navbar, som er på alle sider.

export default function Navbar() {
  const navigate = useNavigate()
  const { bruger, logout } = useAuth()

  async function handleLogout() {
    await logout()
    navigate('/')
  }

  return (
    <div className="CommonHeaderBar">
      <div className="CommonHeaderLeft">
        <h1 id="CommonheaderText">Andreas Bio</h1>
      </div>
      <div className="CommonHeaderRight">
        <button id="CommonButton" onClick={() => navigate('/program')}>Program</button>
        <button id="CommonButton" onClick={() => navigate('/about')}>Om Biografen</button>
        {bruger ? (
          <>
            <button id="CommonButton" onClick={handleLogout}>Log ud</button>
            <span>Hej {bruger.username}</span>

          </>
        ) : (
          <button id="CommonButton" onClick={() => navigate('/login')}>Login</button>
        )}
      </div>
    </div>
  )
}