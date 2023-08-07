import React, { useState } from 'react';
import { Container, Typography, TextField, Button, Link } from '@mui/material';

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errors, setErrors] = useState({ email: '', password: '' });

  const containerStyle = {
    marginTop: '2rem',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  };

  const formStyle = {
    width: '100%',
    marginTop: '1rem',
  };

  const inputStyle = {
    marginBottom: '1rem',
    width: '100%',
  };

  const errorStyle = {
    color: 'red',
    fontSize: '0.8rem',
    marginTop: '0.2rem',
  };

  const submitButtonStyle = {
    marginTop: '1rem',
  };

  const linkStyle = {
    fontSize: '0.9rem',
    textDecoration: 'none',
    marginLeft: '0.5rem',
  };

  const handleLogin = (e) => {
    e.preventDefault();

    let newErrors = { email: '', password: '' };

    if (!email) {
      newErrors.email = 'Email is required';
    }

    if (!password) {
      newErrors.password = 'Password is required';
    }

    if (newErrors.email || newErrors.password) {
      setErrors(newErrors);
      return;
    }

    // Implement your login logic here
  };

  return (
    <Container maxWidth="sm" style={containerStyle}>
      <Typography variant="h4" align="center" gutterBottom>
        Login
      </Typography>
      <form style={formStyle} onSubmit={handleLogin}>
        <TextField
          type="text"
          label="Email"
          variant="outlined"
          style={inputStyle}
          value={email}
          onChange={(e) => {
            setEmail(e.target.value);
            setErrors({ ...errors, email: '' });
          }}
        />
        {errors.email && <div style={errorStyle}>{errors.email}</div>}
        <TextField
          type="password"
          label="Password"
          variant="outlined"
          style={inputStyle}
          value={password}
          onChange={(e) => {
            setPassword(e.target.value);
            setErrors({ ...errors, password: '' });
          }}
        />
        {errors.password && <div style={errorStyle}>{errors.password}</div>}
        <Button variant="contained" color="primary" style={submitButtonStyle} type="submit">
          Login
        </Button>
      </form>
      <div style={{ marginTop: '1rem', textAlign: 'center' }}>
        <Link href="#" style={linkStyle}>
          Forgot Password?
        </Link>
      </div>
      <div style={{ marginTop: '1rem', textAlign: 'center' }}>
        <span><h6>Don't have an account?</h6> </span>
        <Link href="#" style={linkStyle}>
          Sign Up
        </Link>
      </div>
    </Container>
  );
}
export default Login;
