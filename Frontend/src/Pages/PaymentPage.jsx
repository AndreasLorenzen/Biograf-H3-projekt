import '../App.css'
import { Link, useLocation } from 'react-router-dom'
import { useEffect, useRef, useState } from 'react'
import Navbar from '../Components/Navbar'
import { useAuth } from '../Components/Authcontext.jsx'


// Forskellige states
export default function Payment() {
  const { state } = useLocation()
  const { bruger } = useAuth()
  const movie = state?.movie
  const paymentFields = useRef(null)
  const [customer, setCustomer] = useState({ name: '', email: '' })
  const [paymentComplete, setPaymentComplete] = useState(false)
  const [paymentError, setPaymentError] = useState('')
  const [isSubmitting, setIsSubmitting] = useState(false)

  // Automatisk udfyldning
  useEffect(() => {
    setCustomer({
      name: bruger?.username ?? '',
      email: bruger?.email ?? '',
    })
  }, [bruger])

  // Tjekker paymentfields, hvis der fx. er tomme felter, som er påkrævede kalder den reportValidity
  async function handlePayment() {
    const invalidField = paymentFields.current?.querySelector(':invalid')
    if (invalidField) {
      invalidField.reportValidity()
      return
    }

    setPaymentError('')
    setIsSubmitting(true)


    // Her bruger vi fetch til at hente og opdatere data 
    try {
      const response = await fetch(`/api/Person/${bruger.personId}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...bruger, movieId: movie.movieId }),
      })

      if (!response.ok) {
        throw new Error('Filmen kunne ikke gemmes på brugeren. Prøv igen.')
      }

      setPaymentComplete(true)
    } catch (error) {
      setPaymentError(error.message || 'Der opstod en fejl. Prøv igen.')
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    // Dette er lavet med betinget visning, så brugeren ikke bare kan tilgå siden med URL uden at være logget ind
    <>
      <Navbar />
      <main className="PageShell PaymentContent">
        {movie ? (
          <>
            <section className="PaymentMovie">
              <h1>{movie.movieName}</h1>
              <p>Varighed: {movie.movieDuration} minutter</p>
            </section>

            <div className="PaymentForm" ref={paymentFields}>
              <h2>Kontaktoplysninger</h2>
              <label className="PaymentField">
                Navn
                <input
                  autoComplete="name"
                  required
                  value={customer.name}
                  onChange={(event) => setCustomer({ ...customer, name: event.target.value })}
                />
              </label>
              <label className="PaymentField">
                Email
                <input
                  autoComplete="email"
                  required
                  type="email"
                  value={customer.email}
                  onChange={(event) => setCustomer({ ...customer, email: event.target.value })}
                />
              </label>

              <h2>Fiktiv betaling</h2>
              <label className="PaymentField">
                Kortnummer
                <input autoComplete="cc-number" inputMode="numeric" maxLength={19} placeholder="1234 5678 9012 3456" required />
              </label>
              <div className="PaymentCardGrid">
                <label className="PaymentField">
                  Udløbsdato
                  <input autoComplete="cc-exp" type="month" required />
                </label>
                <label className="PaymentField">
                  CVC
                  <input autoComplete="cc-csc" inputMode="numeric" maxLength={4} placeholder="123" required />
                </label>
              </div>
              <button className="PaymentSubmit" type="button" onClick={handlePayment} disabled={isSubmitting || paymentComplete}>
                {isSubmitting ? 'Gemmer...' : 'Betal (demo)'}
              </button>
              {paymentError && <p role="alert">{paymentError}</p>}
              {paymentComplete && <p role="status">Demobetalingen er gennemført for {movie.movieName}.</p>}
            </div>
          </>
        ) : (
          <>
            <h1>Vælg en film</h1>
            <Link to="/program">Gå til programmet</Link>
          </>
        )}
      </main>
    </>
  )
}