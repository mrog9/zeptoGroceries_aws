import { useState } from 'react'
import { useNavigate, Link } from 'react-router-dom'
import { newUserRequest } from '../actions/customer/userRequests'
import '../styles/Auth.css'

function SignupForm() {
  const [username, setUsername] = useState('')
  const [error, setError] = useState('')
  const [success, setSuccess] = useState('')
  const navigate = useNavigate()

  const handleSubmit = (e) => {
    e.preventDefault()

    if (success){

      setError("Account has already been created")

    }else{
    
      if (!username.trim()) {
        setError('Username is required')
        return
      }

      if (username.length < 5 || username.length > 10) {
        setError('Username must between 5 and 10 characters long')
        return
      }

      // Simulate account creation
      console.log('Creating account with username:', username)

      newUserRequest(username)
      .then((resp) => {

        console.log(resp)

        if (!resp.error){

          if (!resp.success){

            setError('Username already exists.')
            return

          }else{

            setSuccess("Your account is now active!")
            setError('')

          }
        }else{

          setError('System error. Please try again later.')
          return

        }


      })
    }

  }

  return (
    <div className="auth-container">
      <div className="auth-hero">
        <h1 className="auth-title">Join Zepto Groceries — Sign up for Freshness in a Zepto</h1>
        <h4 className="auth-tagline">Create your account and get groceries in a flash</h4>
      </div>
      <div className="auth-card">
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="username">Username</label>
            <input
              id="username"
              type="text"
              placeholder="Choose a username"
              value={username}
              onChange={(e) => {
                setUsername(e.target.value)
                setError('')
              }}
              className="form-input"
            />
          </div>
          
          {error && <div className="error-message">{error}</div>}
          {success && <div className="success-message">{success}</div>}
          
          <button type="submit" className="btn btn-primary">
            Create Account
          </button>
        </form>
        
        <p className="auth-link">
          Already have an account? <Link to="/">Login</Link>
        </p>
      </div>
    </div>
  )
}

export default SignupForm
