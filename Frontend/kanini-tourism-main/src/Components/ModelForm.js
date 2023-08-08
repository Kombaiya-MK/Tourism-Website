import React, { useState } from 'react';
import {
  Modal,
  Backdrop,
  Fade,
  TextField,
  Button,
  Grid,
  Paper,
  Typography,
  MenuItem,
} from '@mui/material';
import { useFormik } from 'formik';
import * as yup from 'yup';
import axios from 'axios';

const validationSchema = yup.object().shape({
  customerId: yup.string().required('Customer ID is required'),
  customerName: yup.string().required('Customer Name is required'),
  customerGender: yup.string().required('Customer Gender is required'),
  customerAge: yup.string().required('Customer Age is required'),
  customerStatus: yup.string().required('Customer Status is required'),
});

const ModalForm = ({ isOpen, onClose }) => {
  const [price, setPrice] = useState(0);

  const formik = useFormik({
    initialValues: {
      customerId: '',
      customerName: '',
      customerGender: '',
      customerAge: '',
      customerStatus: '',
    },
    validationSchema: validationSchema,
    onSubmit: async (values) => {
      try {
        const response = await axios.post('http://localhost:5066/api/Booking/AddCustomer', values);
        if (response.status === 201) {
          // Successfully added customer, now generate the price
          const priceResponse = await axios.post('http://localhost:5066/api/Booking/GeneratePrice', {
            price: price,
            discount: 0, // You can set the discount if needed
            quantity: 1, // Assuming you're adding one customer
          });

          if (priceResponse.status === 200) {
            // Display the generated price
            const totalPrice = priceResponse.data.totalPrice;
            alert(`Customer added successfully. Total Price: $${totalPrice}`);
            onClose();
          } else {
            // Handle price generation error
            console.error('Error generating price:', priceResponse.error);
            alert('An error occurred while generating price');
          }
        } else {
          // Handle customer addition error
          console.error('Error adding customer:', response.error);
          alert('An error occurred while adding customer');
        }
      } catch (error) {
        console.error('Error:', error);
        alert('An error occurred');
      }
    },
  });

  const handleCalculatePrice = async () => {
    try {
      const priceResponse = await axios.post('http://localhost:5066/api/Booking/GeneratePrice', {
        price: formik.values.price,
        discount: 0, // You can set the discount if needed
        quantity: 1, // Assuming you're adding one customer
      });

      if (priceResponse.status === 200) {
        // Update the price state
        const totalPrice = priceResponse.data.totalPrice;
        setPrice(totalPrice);
      } else {
        // Handle price generation error
        console.error('Error generating price:', priceResponse.error);
        alert('An error occurred while generating price');
      }
    } catch (error) {
      console.error('Error:', error);
      alert('An error occurred');
    }
  };

  return (
    <Modal
      open={isOpen}
      onClose={onClose}
      closeAfterTransition
      BackdropComponent={Backdrop}
      BackdropProps={{
        timeout: 500,
      }}
    >
      <Fade in={isOpen}>
        <div>
          <Paper elevation={3} style={{ padding: '16px', backgroundColor: '#fff', borderRadius: '8px' }}>
            <Typography variant="h6" gutterBottom>
              Add New Customer
            </Typography>
            <form onSubmit={formik.handleSubmit}>
              <Grid container spacing={2}>
                <Grid item xs={12}>
                  <TextField
                    label="Customer ID"
                    variant="outlined"
                    fullWidth
                    name="customerId"
                    value={formik.values.customerId}
                    onChange={formik.handleChange}
                    error={formik.touched.customerId && Boolean(formik.errors.customerId)}
                    helperText={formik.touched.customerId && formik.errors.customerId}
                  />
                </Grid>
                <Grid item xs={12}>
                  <TextField
                    label="Customer Name"
                    variant="outlined"
                    fullWidth
                    name="customerName"
                    value={formik.values.customerName}
                    onChange={formik.handleChange}
                    error={formik.touched.customerName && Boolean(formik.errors.customerName)}
                    helperText={formik.touched.customerName && formik.errors.customerName}
                  />
                </Grid>
                <Grid item xs={12}>
                  <TextField
                    select
                    label="Customer Gender"
                    variant="outlined"
                    fullWidth
                    name="customerGender"
                    value={formik.values.customerGender}
                    onChange={formik.handleChange}
                    error={formik.touched.customerGender && Boolean(formik.errors.customerGender)}
                    helperText={formik.touched.customerGender && formik.errors.customerGender}
                  >
                    <MenuItem value="Male">Male</MenuItem>
                    <MenuItem value="Female">Female</MenuItem>
                    <MenuItem value="Other">Other</MenuItem>
                  </TextField>
                </Grid>
                <Grid item xs={12}>
                  <TextField
                    label="Customer Age"
                    variant="outlined"
                    fullWidth
                    name="customerAge"
                    value={formik.values.customerAge}
                    onChange={formik.handleChange}
                    error={formik.touched.customerAge && Boolean(formik.errors.customerAge)}
                    helperText={formik.touched.customerAge && formik.errors.customerAge}
                  />
                </Grid>
                <Grid item xs={12}>
                  <TextField
                    label="Customer Status"
                    variant="outlined"
                    fullWidth
                    name="customerStatus"
                    value={formik.values.customerStatus}
                    onChange={formik.handleChange}
                    error={formik.touched.customerStatus && Boolean(formik.errors.customerStatus)}
                    helperText={formik.touched.customerStatus && formik.errors.customerStatus}
                  />
                </Grid>
                <Grid item xs={12}>
                  <Button
                    variant="contained"
                    onClick={handleCalculatePrice}
                  >
                    Calculate Price
                  </Button>
                  <Typography variant="h6" gutterBottom>
                    Price: ${price.toFixed(2)}
                  </Typography>
                </Grid>
                <Grid item xs={12}>
                  <Button
                    variant="contained"
                    type="submit"
                  >
                    Add Customer
                  </Button>
                </Grid>
              </Grid>
            </form>
          </Paper>
        </div>
      </Fade>
    </Modal>
  );
};

export default ModalForm;
