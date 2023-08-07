import React, { useState } from 'react';
import {
  Container,
  Typography,
  Card,
  CardContent,
  CardMedia,
  Grid,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
} from '@mui/material';
import StarRating from './StarRating'; // Provide the correct path to the StarRating component
import ReviewList from './ReviewList'; // Provide the correct path to the ReviewList component

const tourPackageDetails = {
  id: 1,
  name: 'Tokyo Adventure',
  city: 'Tokyo',
  date: '2023-09-15',
  price: 1500,
  speciality: 'Adventure',
  image: 'https://via.placeholder.com/300',
  rating: 4.5,
  reviews: [
    {
      name: 'John Doe',
      rating: 4,
      date: '2023-08-10',
      comment: 'Great experience! Highly recommended.',
    },
    {
      name: 'Jane Smith',
      rating: 5,
      date: '2023-08-12',
      comment: 'Absolutely amazing tour. Loved every moment of it!',
    },
    // Add more reviews
  ],
};

function PackageDetails() {
  const [open, setOpen] = useState(false);

  const handleOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <div>
      <Container>
        <Card>
          <CardMedia
            component="img"
            height="200"
            image={tourPackageDetails.image}
            alt={tourPackageDetails.name}
          />
          <CardContent>
            <Typography variant="h5">{tourPackageDetails.name}</Typography>
            <Typography variant="body2">{tourPackageDetails.city}</Typography>
            <Typography variant="body2">{tourPackageDetails.date}</Typography>
            <Typography variant="body2">${tourPackageDetails.price}</Typography>
            <Typography variant="body2">{tourPackageDetails.speciality}</Typography>
            <StarRating value={tourPackageDetails.rating} />
            <Button variant="outlined" color="primary" onClick={handleOpen}>
              View Reviews
            </Button>
          </CardContent>
        </Card>
      </Container>
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Reviews for {tourPackageDetails.name}</DialogTitle>
        <DialogContent>
          <ReviewList reviews={tourPackageDetails.reviews} />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="primary">
            Close
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}

export default PackageDetails;
