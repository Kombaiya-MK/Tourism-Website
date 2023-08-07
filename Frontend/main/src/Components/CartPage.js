import React, { useState } from 'react';
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

const paperStyle = {
  padding: '16px',
  backgroundColor: '#fff',
  borderRadius: '8px',
};

const CartPage = () => {
  const [selectedPacks, setSelectedPacks] = useState([
    {
      id: 1,
      name: 'Tokyo Exploration',
      startDate: '2023-08-15',
      endDate: '2023-08-20',
      adults: 2,
      children: 1,
      price: 1500,
      image: 'https://via.placeholder.com/150', // Add image URL here
    },
    {
      id: 2,
      name: 'Kyoto Adventure',
      startDate: '2023-09-10',
      endDate: '2023-09-15',
      adults: 1,
      children: 0,
      price: 1200,
      image: 'https://via.placeholder.com/150', // Add image URL here
    },
    // Add more selected packs
  ]);

  const handleRemovePack = (packId) => {
    const updatedPacks = selectedPacks.filter((pack) => pack.id !== packId);
    setSelectedPacks(updatedPacks);
  };

  const handleSelectPack = (packId) => {
    // Logic for selecting a pack
  };

  const getNumberOfDays = (startDate, endDate) => {
    const start = new Date(startDate);
    const end = new Date(endDate);
    const days = (end - start) / (1000 * 60 * 60 * 24) + 1;
    return days;
  };

  const getTotalPrice = () => {
    return selectedPacks.reduce((total, pack) => total + pack.price, 0);
  };

  return (
    <div style={{ margin: '16px' }}>
      <Paper elevation={3} style={paperStyle}>
        <Typography variant="h5" gutterBottom>
          Your Cart
        </Typography>
        <List>
          {selectedPacks.map((pack) => (
            <div key={pack.id}>
              <Card variant="outlined" style={{ marginBottom: '8px' }}>
                <CardMedia component="img" height="150" image={pack.image} alt={pack.name} />
                <CardContent>
                  <Typography variant="h6" gutterBottom>
                    {pack.name}
                  </Typography>
                  <Typography variant="body2">
                    Date Range: {pack.startDate} - {pack.endDate}, {getNumberOfDays(pack.startDate, pack.endDate)} days
                  </Typography>
                  <Typography variant="body2">
                    Adults: {pack.adults}, Children: {pack.children}
                  </Typography>
                  <Typography variant="h6" style={{ marginTop: '8px' }}>
                    Price: ${pack.price}
                  </Typography>
                </CardContent>
                <CardActions>
                  <Tooltip title="Remove from Cart">
                    <IconButton color="secondary" onClick={() => handleRemovePack(pack.id)}>
                      <DeleteIcon />
                    </IconButton>
                  </Tooltip>
                  <Tooltip title="Select Pack">
                    <IconButton color="primary" onClick={() => handleSelectPack(pack.id)}>
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
              <Button variant="contained" color="secondary" fullWidth startIcon={<MonetizationOnIcon />}>
                Proceed to Checkout
              </Button>
            </Tooltip>
          </Grid>
        </Grid>
      </Paper>
    </div>
  );
};

export default CartPage;
