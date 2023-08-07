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

import '../Assets/Styles/AddTourPackageForm.css'; 

const validationSchema = yup.object().shape({
  packageName: yup.string().required('Package name is required'),
  destination: yup.string().required('Destination is required'),
  price: yup.number().required('Price is required').positive('Price must be a positive number'),
  startDate: yup.date().required('Start date is required'),
  endDate: yup.date().required('End date is required'),
  description: yup.string().required('Description is required'),
});

function AddTourPackageForm() {
  const formik = useFormik({
    initialValues: {
      packageName: '',
      destination: '',
      price: '',
      startDate: '',
      endDate: '',
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
        Add Tour Package
      </Typography>
      <Paper elevation={3} className="form-container">
        <form onSubmit={formik.handleSubmit} className="form">
          <Grid container spacing={3}>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Package Name"
                name="packageName"
                value={formik.values.packageName}
                onChange={formik.handleChange}
                error={formik.touched.packageName && formik.errors.packageName}
                helperText={formik.touched.packageName && formik.errors.packageName}
                required
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Destination"
                name="destination"
                value={formik.values.destination}
                onChange={formik.handleChange}
                error={formik.touched.destination && formik.errors.destination}
                helperText={formik.touched.destination && formik.errors.destination}
                required
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Price ($)"
                name="price"
                type="number"
                value={formik.values.price}
                onChange={formik.handleChange}
                error={formik.touched.price && formik.errors.price}
                helperText={formik.touched.price && formik.errors.price}
                required
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Start Date"
                name="startDate"
                type="date"
                value={formik.values.startDate}
                onChange={formik.handleChange}
                error={formik.touched.startDate && formik.errors.startDate}
                helperText={formik.touched.startDate && formik.errors.startDate}
                required
                InputLabelProps={{
                  shrink: true,
                }}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="End Date"
                name="endDate"
                type="date"
                value={formik.values.endDate}
                onChange={formik.handleChange}
                error={formik.touched.endDate && formik.errors.endDate}
                helperText={formik.touched.endDate && formik.errors.endDate}
                required
                InputLabelProps={{
                  shrink: true,
                }}
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
                Add Package
              </Button>
            </Grid>
          </Grid>
        </form>
      </Paper>
    </Container>
  );
}

export default AddTourPackageForm;
