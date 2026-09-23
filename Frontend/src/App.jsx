import './App.css'
import { Routes, Route } from 'react-router-dom';
import HomePage from './Pages/HomePage.jsx';
import LoginPage from './Pages/LoginPage.jsx';
import ErrorPage from './Pages/ErrorPage.jsx'

function App() {
  return (
    <>
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path='*' element={<ErrorPage />} />
      <Route path="/login" element={<LoginPage/>} />
    </Routes>
    </>
  )
}

export default App
