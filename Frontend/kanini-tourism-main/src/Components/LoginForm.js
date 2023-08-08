import React, { useState } from 'react';
import { Container, Typography, TextField, Button, Link, Modal } from '@mui/material';
import * as yup from 'yup';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import SignUpForm from './SignUpForm'; 
import ForgotPassword from './ForgotPassword';

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

const submitButtonStyle = {
  marginTop: '1rem',
};

const linkStyle = {
  fontSize: '0.9rem',
  textDecoration: 'none',
  marginLeft: '0.5rem',
};

const validationSchema = yup.object().shape({
  email: yup.string().email('Invalid email address').required('Email is required'),
  password: yup.string().required('Password is required'),
});

function LoginForm() {
  const [isForgotPasswordModalOpen, setIsForgotPasswordModalOpen] = useState(false);
  const [isSignUpMode, setIsSignUpMode] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const toggleForgotPasswordModal = () => {
    setIsForgotPasswordModalOpen(!isForgotPasswordModalOpen);
  };

  const toggleSignUpMode = () => {
    setIsSignUpMode(!isSignUpMode);
  };

  const handleSubmit = async (e) => {
    console.log(email)
    console.log(password)
    e.preventDefault();
    try {
      const response = await axios.post('https://localhost:7289/api/User/Login', {
        email: email,
        password: password,
      });

      const data = response.data;

      if (data.token) {
        sessionStorage.clear();
        sessionStorage.setItem('email', data.email);
        sessionStorage.setItem('token', data.token);
        sessionStorage.setItem('status', data.status);
        sessionStorage.setItem('role', data.role);

        toast.success('Login successful');
        navigate('/');
      } else {
        toast.error('Login failed');
      }
    } catch (error) {
      toast.error('An error occurred');
    }
  };

  return (
    <Container maxWidth="sm" style={containerStyle}>
      <Typography variant="h4" align="center" gutterBottom>
        {isSignUpMode ? 'Sign Up' : 'Login'}
      </Typography>
      {isSignUpMode ? (
        <SignUpForm toggleSignUpMode={toggleSignUpMode} />
      ) : (
        <form style={formStyle} onSubmit={handleSubmit}>
          <TextField
            type="text"
            label="Email"
            variant="outlined"
            style={inputStyle}
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <TextField
            type="password"
            label="Password"
            variant="outlined"
            style={inputStyle}
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <Button variant="contained" color="primary" style={submitButtonStyle} type="submit">
            {isSignUpMode ? 'Sign Up' : 'Login'}
          </Button>
        </form>
      )}
      <div style={{ marginTop: '1rem', textAlign: 'center' }}>
        {isSignUpMode ? (
          <Link href="#" style={linkStyle} onClick={toggleSignUpMode}>
            Already have an account? Login
          </Link>
        ) : (
          <Link href="#" style={linkStyle} onClick={toggleForgotPasswordModal}>
            Forgot Password?
          </Link>
        )}
      </div>
      <div style={{ marginTop: '1rem', textAlign: 'center' }}>
        <span>
          <h6>{isSignUpMode ? 'Already have an account?' : "Don't have an account?"}</h6>
        </span>
        <Link href="#" style={linkStyle} onClick={toggleSignUpMode}>
          {isSignUpMode ? 'Login' : 'Sign Up'}
        </Link>
      </div>
      <Modal open={isForgotPasswordModalOpen} onClose={toggleForgotPasswordModal}>
        <div style={{ width: 400, backgroundColor: 'white', padding: 20, position: 'absolute', top: '50%', left: '50%', transform: 'translate(-50%, -50%)' }}>
          <ForgotPassword onClose={toggleForgotPasswordModal} />
        </div>
      </Modal>
      <ToastContainer position="bottom-right" autoClose={3000} />
    </Container>
  );
}

export default LoginForm;
