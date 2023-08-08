import React from 'react';
import { TextField, Button, Paper, Typography } from '@mui/material';
import { styled } from '@mui/system';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

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
  const navigate = useNavigate();

  const handleNewBooking = async (formData) => {
    try {
      const newBooking = {
        bookingId: 'id',
        packageId: sessionStorage.getItem('packageId'),
        email: sessionStorage.getItem('email'),
        bookedDate: new Date().toISOString(),
        checkInDate: formData.checkInDate, 
        checkOutDate: formData.checkOutDate, 
        price: formData.price, 
        status: 'Pending', 
        paymentMethod: formData.paymentMethod, 
        noofAdults: formData.adults,
        noofChildren: formData.children,
      };

      await axios.post('http://localhost:5066/api/Booking/AddBooking', newBooking);
      toast.success('Booking successful');
      navigate('/');
    } catch (error) {
      console.error('Error creating booking:', error);
      toast.error('An error occurred while creating booking');
    }
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    const formData = {
      name: event.target.name.value,
      email: event.target.email.value,
      date: event.target.date.value,
      adults: parseInt(event.target.adults.value),
      children: parseInt(event.target.children.value),
    };
    handleNewBooking(formData);
  };

  return (
    <Paper elevation={3} style={paperStyle}>
      <Typography variant="h5" gutterBottom>
        Book {packageName}
      </Typography>
      <form style={formStyle} onSubmit={handleSubmit}>
        <TextField
          label="Name"
          variant="outlined"
          name="name"
          required
        />
        <TextField
          label="Email"
          variant="outlined"
          type="email"
          name="email"
          required
        />
        <TextField
          label="Check-in Date"
          variant="outlined"
          type="datetime-local" 
          name="checkInDate"
          required
        />
        <TextField
          label="Check-out Date"
          variant="outlined"
          type="datetime-local" 
          name="checkOutDate"
          required
        />
        <TextField
          label="Payment Method"
          variant="outlined"
          name="paymentMethod"
          required
        />
        <TextField
          label="Adults"
          variant="outlined"
          type="number"
          name="adults"
          required
        />
        <TextField
          label="Children"
          variant="outlined"
          type="number"
          name="children"
        />
        <Button type="submit" style={buttonStyle}>
          Book Now
        </Button>
      </form>
    </Paper>
  );
};

export default BookPackageForm;
