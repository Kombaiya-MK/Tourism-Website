import React, { useState } from 'react';
import {
  Container,
  Typography,
  TextField,
  Button,
  Grid,
  Paper,
} from '@mui/material';
import axios from 'axios';

import '../Assets/Styles/AddTourPackageForm.css'; 

function AddTourPackageForm() {
  const [formData, setFormData] = useState({
    name: '',
    description: '',
    duration: '',
    locationId: '',
    price: '',
    capacity: '',
    status: '',
  });
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleChange = (event) => {
    const { name, value } = event.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    setIsSubmitting(true);

    try {
      const response = await axios.post(
        'https://localhost:7169/api/TourPack/AddPack',
        {
          name: formData.name,
          description: formData.description,
          duration: formData.duration,
          locationId: formData.locationId,
          price: formData.price,
          capacity: formData.capacity,
          status: formData.status,
        }
      );

      if (response.data) {
        console.log('Tour package added successfully:', response.data);
        // Reset form after successful submission
        setFormData({
          name: '',
          description: '',
          duration: '',
          locationId: '',
          price: '',
          capacity: '',
          status: '',
        });
      } else {
        console.error('Failed to add tour package');
      }
    } catch (error) {
      console.error('An error occurred:', error);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <Container>
      <Typography variant="h2" align="center" gutterBottom>
        Add Tour Package
      </Typography>
      <Paper elevation={3} className="form-container">
        <form onSubmit={handleSubmit} className="form">
          <Grid container spacing={3}>
            <Grid item xs={12}>
              <TextField
                fullWidth
                label="Package Name"
                name="name"
                value={formData.name}
                onChange={handleChange}
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
                value={formData.description}
                onChange={handleChange}
                required
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Duration"
                name="duration"
                value={formData.duration}
                onChange={handleChange}
                required
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Location ID"
                name="locationId"
                value={formData.locationId}
                onChange={handleChange}
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
                onChange={handleChange}
                required
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                fullWidth
                label="Capacity"
                name="capacity"
                type="number"
                value={formData.capacity}
                onChange={handleChange}
                required
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                fullWidth
                label="Status"
                name="status"
                value={formData.status}
                onChange={handleChange}
                required
              />
            </Grid>
            <Grid item xs={12}>
              <Button
                type="submit"
                variant="contained"
                color="primary"
                fullWidth
                disabled={isSubmitting}
              >
                {isSubmitting ? 'Adding...' : 'Add Package'}
              </Button>
            </Grid>
          </Grid>
        </form>
      </Paper>
    </Container>
  );
}

export default AddTourPackageForm;
