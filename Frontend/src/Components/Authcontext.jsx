import { createContext, useContext, useEffect, useState } from 'react'
import { Navigate } from 'react-router-dom'

const AuthContext = createContext(null)

export function AuthProvider({ children }) {
  const [bruger, setBruger] = useState(null)   // null = ikke logget ind
  const [loading, setLoading] = useState(true) // true mens vi spørger /Me

  // Ved opstart: er der allerede en gyldig login-cookie?
  useEffect(() => {
    fetch('/api/Auth/Me')
      .then((res) => (res.ok ? res.json() : null))
      .then(setBruger)
      .catch(() => setBruger(null))
      .finally(() => setLoading(false))
  }, [])

  // Bruges til både 'Login' og 'Register'. Returnerer en fejltekst, eller null hvis det gik godt.
  async function send(action, data) {
    const res = await fetch(`/api/Auth/${action}`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data),
    })
    if (!res.ok) return (await res.text()) || 'Noget gik galt' // fejltekst fra AuthController
    setBruger(await res.json())
    return null
  }

  async function logout() {
    await fetch('/api/Auth/Logout', { method: 'POST' })
    setBruger(null)
  }

  return (
    <AuthContext.Provider value={{ bruger, loading, send, logout }}>
      {children}
    </AuthContext.Provider>
  )
}

export const useAuth = () => useContext(AuthContext)

export function RequireLogin({ children }) {
  const { bruger, loading } = useAuth()
  if (loading) return <p>Indlæser...</p>
  return bruger ? children : <Navigate to="/login" replace />
}