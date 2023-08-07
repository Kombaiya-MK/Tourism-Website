import React, { useState } from 'react';
import { Container, Typography, TextField, Button, Link } from '@mui/material';
import { useFormik } from 'formik';
import * as yup from 'yup';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import ForgotPassword from './ForgotPassword';
import Modal from '@mui/material/Modal';


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

const validationSchema = yup.object().shape({
  email: yup.string().email('Invalid email address').required('Email is required'),
  password: yup.string().required('Password is required'),
});

function LoginForm() {

  const [isForgotPasswordModalOpen, setIsForgotPasswordModalOpen] = useState(false);

  const toggleForgotPasswordModal = () => {
    setIsForgotPasswordModalOpen(!isForgotPasswordModalOpen);
  };
  const navigate = useNavigate();
  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
    },
    validationSchema: validationSchema,
    onSubmit: async (values) => {
      try {
        const response = await axios.post('https://localhost:7289/api/User/Login', values);

        const data = response.data;

        if (data.token) {
          localStorage.clear();
          localStorage.setItem('email', data.email);
          localStorage.setItem('token', data.token);
          localStorage.setItem('status', data.status);
          localStorage.setItem('role', data.role);

          toast.success('Login successful');
          navigate('/');
        } else {
          toast.error('Login failed');
        }
      } catch (error) {
        toast.error('An error occurred');
      }
    },
  });

  return (
    <Container maxWidth="sm" style={containerStyle}>
      <Typography variant="h4" align="center" gutterBottom>
        Login
      </Typography>
      <form style={formStyle} onSubmit={formik.handleSubmit}>
        <TextField
          type="text"
          label="Email"
          variant="outlined"
          style={inputStyle}
          name="email"
          value={formik.values.email}
          onChange={formik.handleChange}
          error={formik.touched.email && !!formik.errors.email}
          helperText={formik.touched.email && formik.errors.email}
        />
        <TextField
          type="password"
          label="Password"
          variant="outlined"
          style={inputStyle}
          name="password"
          value={formik.values.password}
          onChange={formik.handleChange}
          error={formik.touched.password && !!formik.errors.password}
          helperText={formik.touched.password && formik.errors.password}
        />
        <Button variant="contained" color="primary" style={submitButtonStyle} type="submit">
          Login
        </Button>
      </form>
      <div style={{ marginTop: '1rem', textAlign: 'center' }}>
        <Link href="#" style={linkStyle} onClick={toggleForgotPasswordModal}>
          Forgot Password?
        </Link>
      </div>
      <div style={{ marginTop: '1rem', textAlign: 'center' }}>
        <span>
          <h6>Don't have an account?</h6>
        </span>
        <Link href="#" style={linkStyle}>
          Sign Up
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
