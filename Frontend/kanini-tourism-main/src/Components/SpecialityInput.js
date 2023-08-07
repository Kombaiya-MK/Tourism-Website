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
  locationId: Yup.string().required('Location ID is required'),
  locationName: Yup.string().required('Location Name is required'),
  speciality: Yup.string().required('Speciality is required'),
  description: Yup.string().required('Description is required'),
});

function SpecialityInput() {
  const [submitting, setSubmitting] = useState(false);

  const formik = useFormik({
    initialValues: {
      locationId: '',
      locationName: '',
      speciality: '',
      description: '',
    },
    validationSchema: validationSchema,
    onSubmit: async (values, { resetForm }) => {
      try {
        setSubmitting(true);

        const response = await axios.post('https://localhost:7153/api/Location/AddSpeciality', values);

        if (response.status === 201) {
          toast.success('Speciality added successfully!', {
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
        toast.error('Failed to add speciality. Please try again.', {
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
          Add Speciality to Available Cities
        </Typography>
        <ToastContainer />
        <form onSubmit={formik.handleSubmit}>
          <TextField
            label="Location ID"
            fullWidth
            margin="normal"
            name="locationId"
            value={formik.values.locationId}
            onChange={formik.handleChange}
            error={formik.touched.locationId && Boolean(formik.errors.locationId)}
            helperText={formik.touched.locationId && formik.errors.locationId}
          />
          <TextField
            label="Location Name"
            fullWidth
            margin="normal"
            name="locationName"
            value={formik.values.locationName}
            onChange={formik.handleChange}
            error={formik.touched.locationName && Boolean(formik.errors.locationName)}
            helperText={formik.touched.locationName && formik.errors.locationName}
          />
          <TextField
            label="Speciality"
            fullWidth
            margin="normal"
            name="speciality"
            value={formik.values.speciality}
            onChange={formik.handleChange}
            error={formik.touched.speciality && Boolean(formik.errors.speciality)}
            helperText={formik.touched.speciality && formik.errors.speciality}
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
          <Button type="submit" variant="contained" color="primary" disabled={submitting}>
            Add Speciality
          </Button>
        </form>
      </Container>
    </InputContainer>
  );
}

export default SpecialityInput;
