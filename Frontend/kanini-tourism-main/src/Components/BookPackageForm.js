import React, { useState } from 'react';
import { TextField, Button, Grid, Paper, Typography } from '@mui/material';
import { useFormik } from 'formik';
import * as yup from 'yup';

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

const validationSchema = yup.object().shape({
  name: yup.string().required('Name is required'),
  email: yup.string().email('Invalid email address').required('Email is required'),
  date: yup.date().required('Travel date is required'),
  adults: yup
    .number()
    .typeError('Please enter a valid number')
    .positive('Number must be positive')
    .integer('Number must be an integer')
    .required('Number of adults is required'),
  children: yup
    .number()
    .typeError('Please enter a valid number')
    .integer('Number must be an integer')
    .min(0, 'Number of children cannot be negative')
    .required('Number of children is required'),
});

const BookPackageForm = ({ packageName }) => {
  const formik = useFormik({
    initialValues: {
      name: '',
      email: '',
      date: '',
      adults: 1,
      children: 0,
    },
    validationSchema: validationSchema,
    onSubmit: (values) => {
      // Implement your form submission logic here
      console.log('Form submitted:', {
        packageName,
        ...values,
      });
    },
  });

  return (
    <Paper elevation={3} style={paperStyle}>
      <Typography variant="h5" gutterBottom>
        Book {packageName}
      </Typography>
      <form style={formStyle} onSubmit={formik.handleSubmit}>
        <TextField
          label="Name"
          variant="outlined"
          name="name"
          value={formik.values.name}
          onChange={formik.handleChange}
          error={formik.touched.name && formik.errors.name}
          helperText={formik.touched.name && formik.errors.name}
          required
        />
        <TextField
          label="Email"
          variant="outlined"
          type="email"
          name="email"
          value={formik.values.email}
          onChange={formik.handleChange}
          error={formik.touched.email && formik.errors.email}
          helperText={formik.touched.email && formik.errors.email}
          required
        />
        <TextField
          label="Travel Date"
          variant="outlined"
          type="date"
          name="date"
          value={formik.values.date}
          onChange={formik.handleChange}
          error={formik.touched.date && formik.errors.date}
          helperText={formik.touched.date && formik.errors.date}
          required
        />
        <TextField
          label="Adults"
          variant="outlined"
          type="number"
          name="adults"
          value={formik.values.adults}
          onChange={formik.handleChange}
          error={formik.touched.adults && formik.errors.adults}
          helperText={formik.touched.adults && formik.errors.adults}
          required
        />
        <TextField
          label="Children"
          variant="outlined"
          type="number"
          name="children"
          value={formik.values.children}
          onChange={formik.handleChange}
          error={formik.touched.children && formik.errors.children}
          helperText={formik.touched.children && formik.errors.children}
        />
        <Button type="submit" style={buttonStyle}>
          Book Now
        </Button>
      </form>
    </Paper>
  );
};

export default BookPackageForm;
