import React, { useState, useEffect } from 'react';
import axios from 'axios';
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
import StarRating from './StarRating'; 
import '../Assets/Styles/Package.css';

const StyledFavoriteIcon = styled(FavoriteIcon)(({ theme, liked }) => ({
  color: liked ? theme.palette.error.main : 'inherit',
}));

function Packages() {
  const [cityFilter, setCityFilter] = useState('');
  const [priceFilter, setPriceFilter] = useState('');
  const [specialityFilter, setSpecialityFilter] = useState('');
  const [dateRangeFilter, setDateRangeFilter] = useState([null, null]);
  const [openPackageDialog, setOpenPackageDialog] = useState(false);
  const [selectedPackage, setSelectedPackage] = useState(null);
  const [selectedRating, setSelectedRating] = useState(0);
  const [reviewComment, setReviewComment] = useState('');
  const [tourPackagesData, setTourPackagesData] = useState([]);
  const [itinerariesData, setItinerariesData] = useState([]);
  const [feedbackData, setFeedbackData] = useState([]);
  const [serviceType, setServiceType] = useState('');
  const [cartItem, setCartItem] = useState([]);
  const [cartItems, setCartItems] = useState([]);

  useEffect(() => {
    // Fetch tour packages data
    axios.get('https://localhost:7169/api/TourPack/GetAllPacks')
      .then(response => {
        setTourPackagesData(response.data);
      })
      .catch(error => {
        console.error('Error fetching tour packages:', error);
      });

    // Fetch itineraries data
    axios.get('https://localhost:7169/api/TourPack/GetAllTouraries?packid=Pack001')
      .then(response => {
        setItinerariesData(response.data);
      })
      .catch(error => {
        console.error('Error fetching itineraries:', error);
      });

    // Fetch feedback data
    axios.get('https://localhost:7026/api/Feedback/GetFeedbacks')
      .then(response => {
        setFeedbackData(response.data);
      })
      .catch(error => {
        console.error('Error fetching feedbacks:', error);
      });
  }, []);

  const filteredPackages = tourPackagesData
    .filter((pkg) => (!cityFilter || pkg.locationId === cityFilter))
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
    setServiceType('');
  };

  const handleLikeClick = async (packageId) => {
    const updatedPackages = tourPackagesData.map((pkg) =>
      pkg.id === packageId ? { ...pkg, likes: pkg.likes + 1 } : pkg
    );
    setTourPackagesData(updatedPackages);
  };

  const handleRatingChange = (value) => {
    setSelectedRating(value);
  };

  const handleReviewSubmit = async () => {
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
    setTourPackagesData(updatedPackages);

    // Close the dialog and reset the state
    handlePackageClose();
  };

  const handleAddToCart = (packageId) => {
    const selectedPackage = tourPackagesData.find((pkg) => pkg.id === packageId);
    if (selectedPackage) {
      const cart1 = {
        cartId: "Cart001",
        email: 'user@example.com', 
        packId: selectedPackage.id,
        quantity: 1,
        price: selectedPackage.price,
        addedDate: new Date(),
        status: 'pending',
      };
      setCartItem(cart1)
      console.log(cartItem)

      axios.post('https://localhost:7145/api/WishList/AddToCart', cartItem)
        .then((response) => {
          setCartItems([...cartItems, response.data]);
        })
        .catch((error) => {
          console.error('Error adding to cart:', error);
        });
    }
  };

  return (
    <div className="tour-packages-container">
      <Container>
        <Typography variant="h2" align="center" gutterBottom>
          Explore Tour Packages
        </Typography>
        <div className="filter-section">
          <Grid container spacing={2}>
            {/* City Filter */}
            <Grid item xs={12} md={3}>
              <FormControl fullWidth variant="outlined">
                <InputLabel>City</InputLabel>
                <Select value={cityFilter} onChange={(e) => setCityFilter(e.target.value)}>
                  <MenuItem value="">All</MenuItem>
                  {tourPackagesData.map((pack) => (
                    <MenuItem key={pack.packId} value={pack}>
                      {pack}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={12} md={3}>
              <FormControl fullWidth variant="outlined">
                <InputLabel>Price</InputLabel>
                <Select value={priceFilter} onChange={(e) => setPriceFilter(e.target.value)}>
                  <MenuItem value="">All</MenuItem>
                  <MenuItem value={1000}>$1000 or less</MenuItem>
                  <MenuItem value={1500}>$1500 or less</MenuItem>
                  <MenuItem value={2000}>$2000 or less</MenuItem>
                </Select>
              </FormControl>
            </Grid>
            {/* Speciality Filter */}
            <Grid item xs={12} md={3}>
              <FormControl fullWidth variant="outlined">
                <InputLabel>Speciality</InputLabel>
                <Select value={specialityFilter} onChange={(e) => setSpecialityFilter(e.target.value)}>
                  <MenuItem value="">All</MenuItem>
                  {/* Map over unique specialities */}
                  {itinerariesData.map((Itinerary) => (
                    <MenuItem key={Itinerary.itineraryId} value={Itinerary}>
                      {Itinerary}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
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
                    <Button onClick={() => handleAddToCart(pkg.id)}>
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
      <Dialog open={openPackageDialog} onClose={handlePackageClose} maxWidth="md" fullWidth>
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
              <div>
                <Typography variant="h6">Itinerary</Typography>
                <ul>
                  {itinerariesData
                    .filter((item) => item.packId === `Pack00${selectedPackage}`)
                    .map((item) => (
                      <li key={item.itineraryId}>{item.description}</li>
                    ))}
                </ul>
              </div>
              <div>
                <Typography variant="h6">Previous Reviews</Typography>
                <ul>
                  {feedbackData
                    .filter((feedback) => feedback.packId === `Pack00${selectedPackage}`)
                    .map((feedback) => (
                      <li key={feedback.id}>
                        <Typography>
                          <strong>Service Type:</strong> {feedback.serviceType}
                        </Typography>
                        <Typography>
                          <strong>Rating:</strong> {feedback.rating}
                        </Typography>
                        <Typography>
                          <strong>Review:</strong> {feedback.review}
                        </Typography>
                        <Typography>
                          <strong>Date:</strong> {new Date(feedback.feedbackDate).toLocaleDateString()}
                        </Typography>
                        <hr />
                      </li>
                    ))}
                </ul>
              </div>
              <div>
                <Typography variant="h6">Submit Your Review</Typography>
                <FormControl fullWidth variant="outlined">
                  <InputLabel>Service Type</InputLabel>
                  <Select value={serviceType} onChange={(e) => setServiceType(e.target.value)}>
                    <MenuItem value="Food">Food</MenuItem>
                    <MenuItem value="Accommodation">Accommodation</MenuItem>
                    <MenuItem value="Transportation">Transportation</MenuItem>
                    {/* Add more options as needed */}
                  </Select>
                </FormControl>
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
