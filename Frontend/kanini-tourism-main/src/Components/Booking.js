import React, { useState } from 'react';
import {
  Container,
  Typography,
  Card,
  CardContent,
  Table,
  TableContainer,
  TableHead,
  TableBody,
  TableRow,
  TableCell,
  Button,
  Modal,
  Backdrop,
  Fade,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  useMediaQuery,
} from '@mui/material';
import { useTheme } from '@mui/material/styles';
import { styled } from '@mui/system';
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios';
import ModalForm from './ModelForm';

// Styled components
const BookingContainer = styled('div')({
  paddingTop: '2rem',
  paddingBottom: '2rem',
});

const BookingCard = styled(Card)({
  maxWidth: '800px',
  margin: '0 auto',
});

const TableCellBold = styled(TableCell)({
  fontWeight: 'bold',
});

const ModalContainer = styled('div')({
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  height: '100%',
});

const ModalContent = styled('div')({
  backgroundColor: 'white',
  padding: '1rem',
  borderRadius: '8px',
  boxShadow: '0px 3px 6px rgba(0, 0, 0, 0.16)',
});

const ConfirmButton = styled(Button)(({ theme }) => ({
  backgroundColor: theme.palette.error.main,
  color: 'white',
  '&:hover': {
    backgroundColor: theme.palette.error.dark,
  },
}));

function Booking({ isAgent }) {
  const theme = useTheme();
  const [bookings, setBookings] = useState([]);
  const [selectedBooking, setSelectedBooking] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isConfirmModalOpen, setIsConfirmModalOpen] = useState(false);

  const isSmallScreen = useMediaQuery(theme.breakpoints.down('sm'));
  const navigate = useNavigate();

  const handleCancelBooking = async (bookingId) => {
    try {
      await axios.put(`http://localhost:5066/api/Booking/CancelBooking/${bookingId}`);
      const updatedBookings = bookings.map((booking) =>
        booking.bookingId === bookingId ? { ...booking, status: 'Canceled' } : booking
      );
      setBookings(updatedBookings);
      handleCloseConfirmModal();
      toast.success('Booking canceled successfully');
    } catch (error) {
      console.error('Error canceling booking:', error);
      toast.error('An error occurred while canceling booking');
    }
  };

  const handleOpenModal = (bookingId) => {
    setSelectedBooking(bookingId);
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setSelectedBooking(null);
    setIsModalOpen(false);
  };

  const handleOpenConfirmModal = (bookingId) => {
    setSelectedBooking(bookingId);
    setIsConfirmModalOpen(true);
  };

  const handleCloseConfirmModal = () => {
    setSelectedBooking(null);
    setIsConfirmModalOpen(false);
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get('http://localhost:5066/api/Booking/GetBookings');
        setBookings(response.data);
      } catch (error) {
        console.error('Error fetching bookings:', error);
      }
    };
    fetchData();
  }, []);

  const handleNewBooking = async () => {
    try {
      const newBooking = {
        bookingId: 'id',
        packageId: sessionStorage.getItem('packageId'),
        email: sessionStorage.getItem('email'),
        bookedDate: new Date().toISOString(),
        checkInDate: '2023-08-08T06:30:57.827Z',
        checkOutDate: '2023-08-08T06:30:57.827Z',
        price: 0,
        status: 'string',
        paymentMethod: 'string',
        noofAdults: 0,
        noofChildren: 0,
      };

      await axios.post('http://localhost:5066/api/Booking/AddBooking', newBooking);
      toast.success('Booking successful');
      navigate('/');
    } catch (error) {
      console.error('Error creating booking:', error);
      toast.error('An error occurred while creating booking');
    }
  };

  return (
    <BookingContainer>
      <Container>
        <Typography variant="h2" align="center" gutterBottom>
          {isAgent ? 'Agent Bookings' : 'Your Bookings'}
        </Typography>
        <BookingCard>
          <CardContent>
            <TableContainer>
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCellBold>Booking ID</TableCellBold>
                    <TableCellBold>Package Name</TableCellBold> 
                    <TableCellBold>Booked Date</TableCellBold> 
                    <TableCellBold>Status</TableCellBold>
                    <TableCellBold>Actions</TableCellBold>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {bookings.map((booking) => (
                    <TableRow key={booking.bookingId}>
                      <TableCellBold>{booking.bookingId}</TableCellBold>
                      <TableCellBold>{booking.packageName}</TableCellBold> {/* Display Package Name */}
                      <TableCellBold>{new Date(booking.bookedDate).toLocaleDateString()}</TableCellBold> {/* Display Booked Date */}
                      <TableCellBold>
                        {booking.status === 'Confirmed' && (
                          <>
                            <Button onClick={() => handleOpenConfirmModal(booking.bookingId)} color="error">
                              Cancel
                            </Button>
                          </>
                        )}
                      </TableCellBold>
                      <TableCellBold>
                        {booking.status === 'Confirmed' && (
                          <>
                            <Button onClick={() =>handleCancelBooking} color="error">
                              Cancel
                            </Button>
                            <Button onClick={() => navigate(`/invoice/${booking.bookingId}`)} color="primary">
                              View Details
                            </Button>
                          </>
                        )}
                      </TableCellBold>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </CardContent>
        </BookingCard>
        <ToastContainer position="bottom-right" autoClose={3000} />
      </Container>
    </BookingContainer>
  );
}
export default Booking;
