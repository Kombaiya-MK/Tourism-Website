import React, { useState, useEffect } from 'react';
import {
  Paper,
  Typography,
  List,
  ListItem,
  ListItemText,
  Divider,
  IconButton,
  Grid,
  Card,
  CardContent,
  CardActions,
  Button,
  Tooltip,
  CardMedia,
} from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import MonetizationOnIcon from '@mui/icons-material/MonetizationOn';
import axios from 'axios';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { Link } from 'react-router-dom';
import BookPackageForm from './BookPackageForm';

const paperStyle = {
  padding: '16px',
  backgroundColor: '#fff',
  borderRadius: '8px',
};

const CartPage = () => {
  const [cartItems, setCartItems] = useState([]);
  const [proceedToCheckout, setProceedToCheckout] = useState(false);
  useEffect(() => {
    // Fetch cart items data
    axios.get('https://localhost:7145/api/WishList/GetAllItemsIntheCart')
      .then(response => {
        setCartItems(response.data);
      })
      .catch(error => {
        console.error('Error fetching cart items:', error);
      });
  }, []);

  const handleRemovePack = (cartId) => {
    
  };

  const handleSelectPack = (cartId) => {
    
  };

  const getNumberOfDays = (startDate, endDate) => {
    const start = new Date(startDate);
    const end = new Date(endDate);
    const days = (end - start) / (1000 * 60 * 60 * 24) + 1;
    return days;
  };

  const getTotalPrice = () => {
    return cartItems.reduce((total, item) => total + item.price, 0);
  };

  return (
    <div style={{ margin: '16px' }}>
      {proceedToCheckout ? ( // Render BookPackageForm on Proceed to Checkout
        <BookPackageForm packageName="Selected Package Name" /> // Replace with the selected package name
      ) : (
        <Paper elevation={3} style={paperStyle}>
          <Typography variant="h5" gutterBottom>
            Your Cart
          </Typography>
          <List>
          {cartItems.map((item) => (
            <div key={item.id}>
              <Card variant="outlined" style={{ marginBottom: '8px' }}>
                <CardContent>
                  {/* Add other details for each cart item */}
                  <Typography variant="h6" gutterBottom>
                    Pack ID: {item.packId}
                  </Typography>
                  <Typography variant="body2">
                    Price: ${item.price}
                  </Typography>
                </CardContent>
                <CardActions>
                  <Tooltip title="Remove from Cart">
                    <IconButton color="secondary" onClick={() => handleRemovePack(item.cartId)}>
                      <DeleteIcon />
                    </IconButton>
                  </Tooltip>
                  <Tooltip title="Select Pack">
                    <IconButton color="primary" onClick={() => handleSelectPack(item.cartId)}>
                      <CheckCircleOutlineIcon />
                    </IconButton>
                  </Tooltip>
                </CardActions>
              </Card>
            </div>
          ))}
        </List>
          <Typography variant="h6" style={{ marginTop: '16px' }}>
            Total: ${getTotalPrice()}
          </Typography>
          <Grid container spacing={2} style={{ marginTop: '16px' }}>
            <Grid item xs={6}>
              <Tooltip title="Continue Shopping">
                <Button variant="contained" color="primary" fullWidth startIcon={<ShoppingCartIcon />}>
                  Continue Shopping
                </Button>
              </Tooltip>
            </Grid>
            <Grid item xs={6}>
              <Tooltip title="Proceed to Checkout">
                <Button
                  variant="contained"
                  color="secondary"
                  fullWidth
                  startIcon={<MonetizationOnIcon />}
                  onClick={() => setProceedToCheckout(true)} // Set proceedToCheckout to true on click
                >
                  Proceed to Checkout
                </Button>
              </Tooltip>
            </Grid>
          </Grid>
        </Paper>
      )}
      <ToastContainer position="bottom-right" autoClose={3000} />
    </div>
  );
};

export default CartPage;
