import React, { useState } from 'react';
import {
    Container,
    Paper,
    Typography,
    TextField,
    Button,
    FormControl,
} from '@mui/material';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const SignUpForm = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            const response = await fetch('https://localhost:7289/api/User/Register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    email: email,
                    password: password,
                    role: 'user', // Set the role according to your requirements
                    status: 'active', // Set the status according to your requirements
                }),
            });

            if (!response.ok) {
                throw new Error('Registration failed');
            }

            const data = await response.json();
            sessionStorage.setItem('email', data.email);
            sessionStorage.setItem('token', data.token);
            sessionStorage.setItem('status', data.status);
            sessionStorage.setItem('role', data.role);
            toast.success('Registration successful');
            setEmail('');
            setPassword('');
        } catch (error) {
            console.error('Registration error:', error);
            toast.error('Registration failed');
        }
    };

    return (
        <Container maxWidth="sm">
            <Paper elevation={3} style={{ padding: 20, margin: '40px auto', maxWidth: 400 }}>
                <Typography variant="h4" gutterBottom>
                    Sign Up
                </Typography>
                <form onSubmit={handleSubmit}>
                    <FormControl fullWidth margin="normal">
                        <TextField
                            label="Email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </FormControl>
                    <FormControl fullWidth margin="normal">
                        <TextField
                            label="Password"
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </FormControl>
                    <Button type="submit" variant="contained" color="primary" fullWidth>
                        Sign Up
                    </Button>
                </form>
            </Paper>
            <ToastContainer position="bottom-right" autoClose={3000} />
        </Container>
    );
};

export default SignUpForm;
