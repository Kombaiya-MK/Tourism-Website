import React, { useState } from 'react';
import {
  Container,
  Typography,
  Grid,
  Button,
  Card,
  CardMedia,
  CardContent,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Tooltip,
  Rating,
  styled,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  TextField,
} from '@mui/material';
import {
  ShoppingCart as ShoppingCartIcon,
  Book as BookIcon,
  Favorite as FavoriteIcon,
  Send as SendIcon,
  Info as InfoIcon,
} from '@mui/icons-material';
import StarRating from './StarRating'; // Replace with your StarRating component
import PackageDetails from './PackageDetails'; // Import the PackageDetails component
import '../Assets/Styles/Package.css';

const StyledFavoriteIcon = styled(FavoriteIcon)(({ theme, liked }) => ({
  color: liked ? theme.palette.error.main : 'inherit',
}));

const tourPackagesData = [
  {
    id: 1,
    name: 'Tokyo Adventure',
    city: 'Tokyo',
    date: '2023-09-15',
    price: 1500,
    speciality: 'Adventure',
    image: 'https://via.placeholder.com/300',
    rating: 4.5,
    likes: 10,
    reviews: [
      {
        name: 'John Doe',
        rating: 4,
        comment: 'Great experience! Highly recommended.',
      },
      // Add more reviews
    ],
  },
  {
    id: 2,
    name: 'Zen Retreat in Kyoto',
    city: 'Kyoto',
    date: '2023-10-01',
    price: 1200,
    speciality: 'Relaxation',
    image: 'https://via.placeholder.com/300',
    rating: 4.2,
    likes: 5,
    reviews: [
      {
        name: 'Michael Johnson',
        rating: 4,
        comment: 'Relaxing tour. Enjoyed the tranquility of Kyoto.',
      },
      // Add more reviews
    ],
  },
  // Add more tour packages
  {
    id: 3,
    name: 'Osaka Discovery',
    city: 'Osaka',
    date: '2023-10-15',
    price: 1100,
    speciality: 'Culture',
    image: 'https://via.placeholder.com/300',
    rating: 3.8,
    likes: 2,
    reviews: [],
  },
];

const specialities = [...new Set(tourPackagesData.map((pkg) => pkg.speciality))];
const cities = [...new Set(tourPackagesData.map((pkg) => pkg.city))];

function Packages() {
  const [cityFilter, setCityFilter] = useState('');
  const [priceFilter, setPriceFilter] = useState('');
  const [specialityFilter, setSpecialityFilter] = useState('');
  const [dateRangeFilter, setDateRangeFilter] = useState([null, null]);
  const [openPackageDialog, setOpenPackageDialog] = useState(false);
  const [selectedPackage, setSelectedPackage] = useState(null);
  const [selectedRating, setSelectedRating] = useState(0);
  const [reviewComment, setReviewComment] = useState('');

  const filteredPackages = tourPackagesData
    .filter((pkg) => (!cityFilter || pkg.city === cityFilter))
    .filter((pkg) => (!priceFilter || pkg.price <= priceFilter))
    .filter((pkg) => (!specialityFilter || pkg.speciality === specialityFilter))
    .filter((pkg) => {
      const [startDate, endDate] = dateRangeFilter;
      if (!startDate || !endDate) return true;
      const packageDate = new Date(pkg.date);
      return packageDate >= startDate && packageDate <= endDate;
    });

  const handlePackageOpen = (packageId) => {
    setSelectedPackage(packageId);
    setOpenPackageDialog(true);
  };

  const handlePackageClose = () => {
    setOpenPackageDialog(false);
    setSelectedPackage(null);
    setSelectedRating(0);
    setReviewComment('');
  };

  const handleLikeClick = (packageId) => {
    const updatedPackages = tourPackagesData.map((pkg) =>
      pkg.id === packageId ? { ...pkg, likes: pkg.likes + 1 } : pkg
    );
    tourPackagesData = updatedPackages;
  };

  const handleRatingChange = (value) => {
    setSelectedRating(value);
  };

  const handleReviewSubmit = () => {
    const updatedPackages = tourPackagesData.map((pkg) =>
      pkg.id === selectedPackage
        ? {
            ...pkg,
            reviews: [
              ...pkg.reviews,
              {
                name: 'User',
                rating: selectedRating,
                comment: reviewComment,
              },
            ],
          }
        : pkg
    );
    tourPackagesData = updatedPackages;

    // Close the dialog and reset the state
    handlePackageClose();
  };

  return (
    <div className="tour-packages-container">
      <Container>
        <Typography variant="h2" align="center" gutterBottom>
          Explore Tour Packages
        </Typography>
        <div className="filter-section">
          <Grid container spacing={2}>
            <Grid item xs={12} md={3}>
              <FormControl fullWidth variant="outlined">
                <InputLabel>City</InputLabel>
                <Select value={cityFilter} onChange={(e) => setCityFilter(e.target.value)}>
                  <MenuItem value="">All</MenuItem>
                  {cities.map((city) => (
                    <MenuItem key={city} value={city}>
                      {city}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={12} md={3}>
              <FormControl fullWidth variant="outlined">
                <InputLabel>Price</InputLabel>
                <Select
                  value={priceFilter}
                  onChange={(e) => setPriceFilter(e.target.value)}
                >
                  <MenuItem value="">All</MenuItem>
                  <MenuItem value={1000}>$1000 or less</MenuItem>
                  <MenuItem value={1500}>$1500 or less</MenuItem>
                  <MenuItem value={2000}>$2000 or less</MenuItem>
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={12} md={3}>
              <FormControl fullWidth variant="outlined">
                <InputLabel>Speciality</InputLabel>
                <Select
                  value={specialityFilter}
                  onChange={(e) => setSpecialityFilter(e.target.value)}
                >
                  <MenuItem value="">All</MenuItem>
                  {specialities.map((speciality) => (
                    <MenuItem key={speciality} value={speciality}>
                      {speciality}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={12} md={3}>
              <TextField
                fullWidth
                variant="outlined"
                label="Date Range"
                type="date"
                value={dateRangeFilter[0] || ''}
                onChange={(e) =>
                  setDateRangeFilter([new Date(e.target.value), dateRangeFilter[1]])
                }
                InputLabelProps={{
                  shrink: true,
                }}
              />
              <TextField
                fullWidth
                variant="outlined"
                type="date"
                value={dateRangeFilter[1] || ''}
                onChange={(e) =>
                  setDateRangeFilter([dateRangeFilter[0], new Date(e.target.value)])
                }
                InputLabelProps={{
                  shrink: true,
                }}
              />
            </Grid>
          </Grid>
        </div>
        <Grid container spacing={4}>
          {filteredPackages.map((pkg) => (
            <Grid item xs={12} md={4} key={pkg.id}>
              <Card>
                <CardMedia component="img" height={200} image={pkg.image} alt={pkg.name} />
                <CardContent>
                  <Typography variant="h5">{pkg.name}</Typography>
                  <Typography variant="body2">{pkg.city}</Typography>
                  <Typography variant="body2">{pkg.date}</Typography>
                  <Typography variant="body2">${pkg.price}</Typography>
                  <Typography variant="body2">{pkg.speciality}</Typography>
                  <div className="action-buttons">
                    <Tooltip title={pkg.likes > 0 ? 'Liked' : 'Like'}>
                      <Button onClick={() => handleLikeClick(pkg.id)}>
                        <StyledFavoriteIcon liked={pkg.likes > 0} />
                      </Button>
                    </Tooltip>
                    <Button>
                      <ShoppingCartIcon />
                    </Button>
                    <Button>
                      <BookIcon />
                    </Button>
                    <Button onClick={() => handlePackageOpen(pkg.id)}>
                      <SendIcon />
                    </Button>
                    <Button onClick={() => handlePackageOpen(pkg.id)}>
                      <InfoIcon />
                    </Button>
                  </div>
                  <StarRating rating={pkg.rating} />
                  <Button onClick={() => handlePackageOpen(pkg.id)}>View Details</Button>
                </CardContent>
              </Card>
            </Grid>
          ))}
        </Grid>
      </Container>
      {/* Package Details Dialog */}
      <Dialog open={openPackageDialog} onClose={handlePackageClose} maxWidth="sm" fullWidth>
        <DialogTitle>Package Details</DialogTitle>
        <DialogContent>
          {selectedPackage !== null && (
            <div>
              <Typography variant="h5">{tourPackagesData[selectedPackage - 1].name}</Typography>
              <Typography variant="body2">{tourPackagesData[selectedPackage - 1].city}</Typography>
              <Typography variant="body2">{tourPackagesData[selectedPackage - 1].date}</Typography>
              <Typography variant="body2">${tourPackagesData[selectedPackage - 1].price}</Typography>
              <Typography variant="body2">{tourPackagesData[selectedPackage - 1].speciality}</Typography>
              <Typography variant="body2">
                Average Rating: {tourPackagesData[selectedPackage - 1].rating}
              </Typography>
              <StarRating rating={selectedRating} onChange={handleRatingChange} />
              <TextField
                label="Your Review"
                multiline
                rows={4}
                fullWidth
                value={reviewComment}
                onChange={(e) => setReviewComment(e.target.value)}
              />
            </div>
          )}
        </DialogContent>
        <DialogActions>
          <Button onClick={handlePackageClose} color="primary">
            Cancel
          </Button>
          <Button onClick={handleReviewSubmit} color="primary">
            Submit Review
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}

export default Packages;
