import React, { useState } from 'react';
import { Container, Typography, TextField, Button } from '@mui/material';
import { styled } from '@mui/system';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const InputContainer = styled('div')({
  paddingTop: '2rem',
  paddingBottom: '2rem',
  textAlign: 'center',
});

const validationSchema = Yup.object({
  location: Yup.string().required('Location is required'),
  description: Yup.string().required('Description is required'),
  latitude: Yup.string().required('Latitude is required'),
  longitude: Yup.string().required('Longitude is required'),
  locationType: Yup.string().required('Location Type is required'),
});

function LocationInput() {
  const [submitting, setSubmitting] = useState(false);

  const formik = useFormik({
    initialValues: {
      location: '',
      description: '',
      latitude: '',
      longitude: '',
      locationType: '',
    },
    validationSchema: validationSchema,
    onSubmit: async (values, { resetForm }) => {
        try {
          setSubmitting(true);

        const dummyUrl = 'https://localhost:7153/api/Location/AddLocation';
        const response = await axios.post(dummyUrl, values);

        if (response.status === 201) {
          toast.success('Location submitted successfully!', {
            position: 'top-right',
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
          });
          resetForm();
          setSubmitting(false);
        }
      } catch (error) {
        toast.error('Failed to submit location. Please try again.', {
          position: 'top-right',
          autoClose: 3000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
        });
        setSubmitting(false);
      }
    },
  });

  return (
    <InputContainer>
      <Container>
        <Typography variant="h4" gutterBottom>
          Enter Location Information
        </Typography>
        <ToastContainer />
        <form onSubmit={formik.handleSubmit}>
          <TextField
            label="Location"
            fullWidth
            margin="normal"
            name="location"
            value={formik.values.location}
            onChange={formik.handleChange}
            error={formik.touched.location && Boolean(formik.errors.location)}
            helperText={formik.touched.location && formik.errors.location}
          />
          <TextField
            label="Description"
            fullWidth
            margin="normal"
            name="description"
            value={formik.values.description}
            onChange={formik.handleChange}
            error={formik.touched.description && Boolean(formik.errors.description)}
            helperText={formik.touched.description && formik.errors.description}
          />
          <TextField
            label="Latitude"
            fullWidth
            margin="normal"
            name="latitude"
            value={formik.values.latitude}
            onChange={formik.handleChange}
            error={formik.touched.latitude && Boolean(formik.errors.latitude)}
            helperText={formik.touched.latitude && formik.errors.latitude}
          />
          <TextField
            label="Longitude"
            fullWidth
            margin="normal"
            name="longitude"
            value={formik.values.longitude}
            onChange={formik.handleChange}
            error={formik.touched.longitude && Boolean(formik.errors.longitude)}
            helperText={formik.touched.longitude && formik.errors.longitude}
          />
          <TextField
            label="Location Type"
            fullWidth
            margin="normal"
            name="locationType"
            value={formik.values.locationType}
            onChange={formik.handleChange}
            error={formik.touched.locationType && Boolean(formik.errors.locationType)}
            helperText={formik.touched.locationType && formik.errors.locationType}
          />
          <Button type="submit" variant="contained" color="primary" disabled={submitting}>
            Submit
          </Button>
        </form>
      </Container>
    </InputContainer>
  );
}

export default LocationInput;
