import { useState } from 'react'
import '../App.css'
import Navbar from '../Components/Navbar'

export default function AdminPage() {
    const adminName = "admin"
    const adminpassword = "admin"
    const [adminNameLogin, setAdminNameLogin] = useState('') 
    const [adminPasswordLogin, setAdminPasswordLogin] = useState('') 


    const [username, setUsername] = useState('')
    const [password, setPassword] = useState('')
    const [adminlevel, setAdminlevel] = useState('')
    
    const [movieName, setMovieName] = useState('')
    const [movieDuration, setMovieDuration] = useState('')

    

    const createAdmin = async () => {
        try {
            const response = await fetch(`/api/admin/createAdmin`, {
                method: `POST`,
                headers: {'Content-Type': `application/json`},
                body: JSON.stringify({
                    username: username,
                    password: password,
                    adminlevel: adminlevel
                }),
            })
            const data = await response.json()
        }
    
            
        catch (err) {

        }
    }
    const CreateMovie = async () => {
        try {
            const response = await fetch(`/api/Movie/createMovie`,
                {
                    method:`POST`,
                    headers: {'Content-Type': `application/json`},
                    body: JSON.stringify({
                      movieName: movieName,
                      movieDuration: movieDuration,
                    }),
                }
            )
            const data = await response.json()
        }
        catch (err){
            
        }
    }
    return (
        <>
        <Navbar/>
        
        {(adminNameLogin !== adminName || adminPasswordLogin !== adminpassword) &&
        <div>
        <input type="text" placeholder='Brugernavn' value={adminNameLogin} onChange={(e) => setAdminNameLogin(e.target.value)}/>
        <input type="password" placeholder='password' value={adminPasswordLogin} onChange={(e) => setAdminPasswordLogin(e.target.value)}/>
        </div>
        }   

        {adminNameLogin === adminName && adminPasswordLogin === adminpassword &&
        <div>
            <div>
                <input type="text" value={username} placeholder='brugernavn' onChange={(e) => setUsername(e.target.value)}/>
                <input type="password" value={password} placeholder='password' onChange={(e) => setPassword(e.target.value)}/>
                <input type="number" value={adminlevel} placeholder='adminlevel' onChange={(e) => setAdminlevel(e.target.value)}/>
                <button onClick={createAdmin}>Opret admin</button>
            </div>
            <div>
                <input type="text" value={movieName} placeholder='MovieName' onChange={(e) => setMovieName(e.target.value)}/>
                <input type="number" value={movieDuration} placeholder='MovieDuration' onChange={(e) => setMovieDuration(e.target.value)}/>
                <button onClick={CreateMovie}>Opret film</button>
            </div>
        </div>
        }

        </>
    )
}