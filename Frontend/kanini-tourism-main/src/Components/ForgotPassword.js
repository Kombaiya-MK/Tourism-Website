import React, { useState } from 'react';
import { Container, Typography, TextField, Button, CircularProgress } from '@mui/material';
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
  const [email, setEmail] = useState('');
  const [code, setCode] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleSubmit = async (event) => {
    event.preventDefault();

    try {
      setIsSubmitting(true);

      if (!isCodeSent) {
        // Send verification code request
        await fetch('https://localhost:7289/api/User/RequestVerificationCode', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ email }),
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
          body: JSON.stringify({ email, code }),
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
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <Container maxWidth="sm" style={containerStyle}>
      <Typography variant="h4" align="center" gutterBottom>
        {isCodeSent ? 'Enter Verification Code' : 'Forgot Password'}
      </Typography>
      <form style={formStyle} onSubmit={handleSubmit}>
        <TextField
          type="text"
          label="Email"
          variant="outlined"
          style={inputStyle}
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          disabled={isCodeSent}
        />
        {isCodeSent && (
          <div style={verificationCodeStyle}>
            <TextField
              type="number"
              label="Verification Code"
              variant="outlined"
              style={inputStyle}
              value={code}
              onChange={(e) => setCode(e.target.value)}
            />
          </div>
        )}
        <Button
          variant="contained"
          color="primary"
          style={submitButtonStyle}
          type="submit"
          disabled={!email || (isCodeSent && !code) || isSubmitting}
        >
          {isCodeSent ? (isSubmitting ? <CircularProgress size={24} /> : 'Verify') : 'Send Verification Code'}
        </Button>
      </form>
      {isCodeSent && (
        <div style={loaderStyle}>
          {isSubmitting && <CircularProgress />}
        </div>
      )}
      <ToastContainer position="bottom-right" autoClose={3000} />
    </Container>
  );
}

export default ForgotPassword;
