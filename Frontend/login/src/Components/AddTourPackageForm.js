import React, { useState } from 'react';
import {
  Container,
  Typography,
  TextField,
  Button,
  Grid,
  Paper,
} from '@mui/material';

import '../Assets/Styles/AddTourPackageForm.css'; // Create a CSS file for styling

function AddTourPackageForm() {
  const [formData, setFormData] = useState({
    packageName: '',
    destination: '',
    price: '',
    startDate: '',
    endDate: '',
    description: '',
  });

  const [errors, setErrors] = useState({
    packageName: '',
    destination: '',
    price: '',
    startDate: '',
    endDate: '',
    description: '',
  });

  const handleSubmit = (e) => {
    e.preventDefault();
    if (validateForm()) {
      // Handle form submission, e.g. send data to backend
      console.log('Form submitted:', formData);
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const validateForm = () => {
    let isValid = true;
    const newErrors = {};

    if (!formData.packageName) {
      newErrors.packageName = 'Package name is required';
      isValid = false;
    }

    if (!formData.destination) {
      newErrors.destination = 'Destination is required';
      isValid = false;
    }

    if (!formData.price) {
      newErrors.price = 'Price is required';
      isValid = false;
    } else if (isNaN(formData.price) || +formData.price <= 0) {
      newErrors.price = 'Invalid price';
      isValid = false;
    }

    if (!formData.startDate) {
      newErrors.startDate = 'Start date is required';
      isValid = false;
    }

    if (!formData.endDate) {
      newErrors.endDate = 'End date is required';
      isValid = false;
    }

    if (!formData.description) {
      newErrors.description = 'Description is required';
      isValid = false;
    }

    setErrors(newErrors);
    return isValid;
  };

  return (
    <Container>
      <Typography variant="h2" align="center" gutterBottom>
        Add Tour Package
      </Typography>
      <Paper elevation={3} className="form-container">
        <form onSubmit={handleSubmit} className="form">
          <Grid container spacing={3}>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Package Name"
                name="packageName"
                value={formData.packageName}
                onChange={handleInputChange}
                error={!!errors.packageName}
                helperText={errors.packageName}
                required
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Destination"
                name="destination"
                value={formData.destination}
                onChange={handleInputChange}
                error={!!errors.destination}
                helperText={errors.destination}
                required
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Price ($)"
                name="price"
                type="number"
                value={formData.price}
                onChange={handleInputChange}
                error={!!errors.price}
                helperText={errors.price}
                required
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Start Date"
                name="startDate"
                type="date"
                value={formData.startDate}
                onChange={handleInputChange}
                error={!!errors.startDate}
                helperText={errors.startDate}
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
                value={formData.endDate}
                onChange={handleInputChange}
                error={!!errors.endDate}
                helperText={errors.endDate}
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
                value={formData.description}
                onChange={handleInputChange}
                error={!!errors.description}
                helperText={errors.description}
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
