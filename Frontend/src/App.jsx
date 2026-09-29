import './App.css'
import { Routes, Route } from 'react-router-dom';
import HomePage from './Pages/HomePage.jsx';
import LoginPage from './Pages/LoginPage.jsx';
import ErrorPage from './Pages/ErrorPage.jsx'
import { RequireLogin } from './Components/Authcontext.jsx'
import PaymentPage from './Pages/PaymentPage.jsx'
import ProgramPage from './Pages/ProgramPage.jsx';
import AdminPage from './Pages/AdminPage.jsx';


function App() {
  return (
    <>
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path='*' element={<ErrorPage />} />
      <Route path="/login" element={<LoginPage/>} />
      <Route path="/payment" element={<RequireLogin><PaymentPage /></RequireLogin>} />
      <Route path="/program" element={<ProgramPage/>}/>
      <Route path="/admin" element={<AdminPage/>}/> 
    </Routes>
    </>
  )
}

export default App
