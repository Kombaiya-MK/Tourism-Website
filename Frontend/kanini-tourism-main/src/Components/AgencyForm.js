import React, { useState } from 'react';
import {
  Container,
  Typography,
  TextField,
  Button,
  Grid,
  Paper,
} from '@mui/material';
import { useFormik } from 'formik';
import * as yup from 'yup';

import '../Assets/Styles/AgencyForm.css'; // Create a CSS file for styling

const validationSchema = yup.object().shape({
  agencyName: yup.string().required('Agency name is required'),
  ownerName: yup.string().required("Owner's name is required"),
  email: yup.string().email('Invalid email address').required('Email is required'),
  phone: yup.string().required('Phone number is required'),
  city: yup.string().required('City is required'),
  country: yup.string().required('Country is required'),
  description: yup.string().required('Description is required'),
});

function AgencyForm() {
  const formik = useFormik({
    initialValues: {
      agencyName: '',
      ownerName: '',
      email: '',
      phone: '',
      city: '',
      country: '',
      description: '',
    },
    validationSchema: validationSchema,
    onSubmit: (values) => {
      // Handle form submission, e.g. send data to backend
      console.log('Form submitted:', values);
    },
  });

  return (
    <Container>
      <Typography variant="h2" align="center" gutterBottom>
        Start Your Travel Agency
      </Typography>
      <Paper elevation={3} className="form-container">
        <form onSubmit={formik.handleSubmit} className="form">
          <Grid container spacing={3}>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Agency Name"
                name="agencyName"
                value={formik.values.agencyName}
                onChange={formik.handleChange}
                error={formik.touched.agencyName && formik.errors.agencyName}
                helperText={formik.touched.agencyName && formik.errors.agencyName}
                required
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Owner's Name"
                name="ownerName"
                value={formik.values.ownerName}
                onChange={formik.handleChange}
                error={formik.touched.ownerName && formik.errors.ownerName}
                helperText={formik.touched.ownerName && formik.errors.ownerName}
                required
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Email"
                name="email"
                type="email"
                value={formik.values.email}
                onChange={formik.handleChange}
                error={formik.touched.email && formik.errors.email}
                helperText={formik.touched.email && formik.errors.email}
                required
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Phone"
                name="phone"
                value={formik.values.phone}
                onChange={formik.handleChange}
                error={formik.touched.phone && formik.errors.phone}
                helperText={formik.touched.phone && formik.errors.phone}
                required
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="City"
                name="city"
                value={formik.values.city}
                onChange={formik.handleChange}
                error={formik.touched.city && formik.errors.city}
                helperText={formik.touched.city && formik.errors.city}
                required
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Country"
                name="country"
                value={formik.values.country}
                onChange={formik.handleChange}
                error={formik.touched.country && formik.errors.country}
                helperText={formik.touched.country && formik.errors.country}
                required
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                fullWidth
                label="Description"
                name="description"
                multiline
                rows={4}
                value={formik.values.description}
                onChange={formik.handleChange}
                error={formik.touched.description && formik.errors.description}
                helperText={formik.touched.description && formik.errors.description}
                required
              />
            </Grid>
            <Grid item xs={12}>
              <Button
                type="submit"
                variant="contained"
                color="primary"
                fullWidth
              >
                Submit
              </Button>
            </Grid>
          </Grid>
        </form>
      </Paper>
    </Container>
  );
}

export default AgencyForm;
