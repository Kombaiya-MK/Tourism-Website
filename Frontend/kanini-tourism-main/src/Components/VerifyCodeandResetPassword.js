import React, { useState } from 'react';
import { Container, Typography, TextField, Button, CircularProgress } from '@mui/material';
import * as yup from 'yup';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios';

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

const validationSchema = yup.object().shape({
  code: yup.number().required('Verification code is required'),
  password: yup.string().required('Password is required'),
});

function VerifyCodeAndResetPassword() {
  const [isCodeVerified, setIsCodeVerified] = useState(false);
  const [code, setCode] = useState('');
  const [password, setPassword] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleSubmit = async (event) => {
    event.preventDefault();
    
    try {
      setIsSubmitting(true);

      if (!isCodeVerified) {
        // Validate verification code
        const email = localStorage.getItem('email'); // Get email from local storage
        const verificationData = {
          email: email,
          code: code,
        };

        const verificationResponse = await axios.post(
          'https://localhost:7289/api/User/ValidateVerificationCode',
          verificationData
        );

        const verificationResult = verificationResponse.data;

        if (verificationResult.isValid) {
          toast.success('Verification code is valid. You can now reset your password.');
          setIsCodeVerified(true);
        } else {
          toast.error('Invalid verification code');
        }
      } else {
        // Reset password
        const email = localStorage.getItem('email'); // Get email from local storage
        const passwordData = {
          email: email,
          password: password,
        };

        const passwordUpdateResponse = await axios.post(
          'https://localhost:7289/api/User/UpdatePassword',
          passwordData
        );

        if (passwordUpdateResponse.data.success) {
          toast.success('Password updated successfully');
        } else {
          toast.error('Failed to update password');
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
        {isCodeVerified ? 'Reset Password' : 'Verify Code'}
      </Typography>
      <form style={formStyle} onSubmit={handleSubmit}>
        {!isCodeVerified && (
          <TextField
            type="number"
            label="Verification Code"
            variant="outlined"
            style={inputStyle}
            value={code}
            onChange={(e) => setCode(e.target.value)}
            error={code.trim() === '' && 'Verification code is required'}
          />
        )}
        {isCodeVerified && (
          <TextField
            type="password"
            label="New Password"
            variant="outlined"
            style={inputStyle}
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            error={password.trim() === '' && 'Password is required'}
          />
        )}
        <Button
          variant="contained"
          color="primary"
          style={submitButtonStyle}
          type="submit"
          disabled={isSubmitting || (!isCodeVerified && code.trim() === '') || (isCodeVerified && password.trim() === '')}
        >
          {isCodeVerified ? (isSubmitting ? <CircularProgress size={24} /> : 'Reset Password') : 'Verify Code'}
        </Button>
      </form>
      <ToastContainer position="bottom-right" autoClose={3000} />
    </Container>
  );
}

export default VerifyCodeAndResetPassword;
