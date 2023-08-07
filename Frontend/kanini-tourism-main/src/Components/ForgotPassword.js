import React, { useState } from 'react';
import { Container, Typography, TextField, Button, CircularProgress } from '@mui/material';
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

const verificationCodeStyle = {
  display: 'flex',
  flexDirection: 'column',
  alignItems: 'center',
};

const loaderStyle = {
  marginTop: '1rem',
};

const validationSchema = yup.object().shape({
  email: yup.string().email('Invalid email address').required('Email is required'),
  code: yup.number().required('Verification code is required'),
});

function ForgotPassword() {
  const [isCodeSent, setIsCodeSent] = useState(false);

  const formik = useFormik({
    initialValues: {
      email: '',
      code: '',
    },
    validationSchema: validationSchema,
    onSubmit: async (values) => {
      try {
        if (!isCodeSent) {
          // Send verification code request
          await fetch('https://localhost:7289/api/User/RequestVerificationCode', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email: values.email }),
          });

          setIsCodeSent(true);
          toast.success('Verification code sent to your email');
        } else {
          // Validate verification code
          const response = await fetch('https://localhost:7289/api/User/ValidateVerificationCode', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(values),
          });

          const data = await response.json();

          if (data.isValid) {
            toast.success('Verification successful');
          } else {
            toast.error('Invalid verification code');
          }
        }
      } catch (error) {
        toast.error('An error occurred');
      }
    },
  });

  return (
    <Container maxWidth="sm" style={containerStyle}>
      <Typography variant="h4" align="center" gutterBottom>
        {isCodeSent ? 'Enter Verification Code' : 'Forgot Password'}
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
          disabled={isCodeSent}
        />
        {isCodeSent && (
          <div style={verificationCodeStyle}>
            <TextField
              type="number"
              label="Verification Code"
              variant="outlined"
              style={inputStyle}
              name="code"
              value={formik.values.code}
              onChange={formik.handleChange}
              error={formik.touched.code && !!formik.errors.code}
              helperText={formik.touched.code && formik.errors.code}
            />
          </div>
        )}
        <Button
          variant="contained"
          color="primary"
          style={submitButtonStyle}
          type="submit"
          disabled={!formik.isValid || (isCodeSent && !formik.values.code)}
        >
          {isCodeSent ? 'Verify' : 'Send Verification Code'}
        </Button>
      </form>
      {isCodeSent && (
        <div style={loaderStyle}>
          {formik.isSubmitting && <CircularProgress />}
        </div>
      )}
      <ToastContainer position="bottom-right" autoClose={3000} />
    </Container>
  );
}

export default ForgotPassword;
