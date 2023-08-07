import React, { useState } from 'react';
import { TextField, Button, Grid, Paper, Typography } from '@mui/material';

const paperStyle = {
  padding: '16px',
  backgroundColor: '#fff',
  borderRadius: '8px',
};

const formStyle = {
  display: 'flex',
  flexDirection: 'column',
  gap: '16px',
};

const buttonStyle = {
  backgroundColor: '#007bff',
  color: '#fff',
  '&:hover': {
    backgroundColor: '#0056b3',
  },
};

const BookPackageForm = ({ packageName }) => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [date, setDate] = useState('');
  const [adults, setAdults] = useState(1);
  const [children, setChildren] = useState(0);

  const handleFormSubmit = (e) => {
    e.preventDefault();
    // Implement your form submission logic here
    console.log('Form submitted:', {
      packageName,
      name,
      email,
      date,
      adults,
      children,
    });
  };

  return (
    <Paper elevation={3} style={paperStyle}>
      <Typography variant="h5" gutterBottom>
        Book {packageName}
      </Typography>
      <form style={formStyle} onSubmit={handleFormSubmit}>
        <TextField
          label="Name"
          variant="outlined"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
        />
        <TextField
          label="Email"
          variant="outlined"
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
        <TextField
          label="Travel Date"
          variant="outlined"
          type="date"
          value={date}
          onChange={(e) => setDate(e.target.value)}
          required
        />
        <TextField
          label="Adults"
          variant="outlined"
          type="number"
          value={adults}
          onChange={(e) => setAdults(e.target.value)}
          required
        />
        <TextField
          label="Children"
          variant="outlined"
          type="number"
          value={children}
          onChange={(e) => setChildren(e.target.value)}
        />
        <Button type="submit" style={buttonStyle}>
          Book Now
        </Button>
      </form>
    </Paper>
  );
};

export default BookPackageForm;
