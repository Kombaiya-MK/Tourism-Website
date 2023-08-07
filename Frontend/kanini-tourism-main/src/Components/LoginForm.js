import React, { useState } from 'react';
import { Container, Typography, TextField, Button, Link } from '@mui/material';
import { useFormik } from 'formik';
import * as yup from 'yup';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

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
  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
    },
    validationSchema: validationSchema,
    onSubmit: async (values) => {
      try {
        const response = await fetch('https://localhost:7289/api/User/Login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(values),
        });

        const data = await response.json();

        if (data.token) {
          toast.success('Login successful');
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
        <Link href="#" style={linkStyle}>
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
      <ToastContainer position="bottom-right" autoClose={3000} />
    </Container>
  );
}

export default LoginForm;
