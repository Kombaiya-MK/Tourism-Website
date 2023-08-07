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

// Sample booking data for users
const sampleUserBookings = [
  { id: 1, packageName: 'Tokyo Tour', agent: 'John Doe', date: '2023-08-10', status: 'Confirmed', amount: 300 },
  { id: 2, packageName: 'Kyoto Experience', agent: 'Jane Smith', date: '2023-08-15', status: 'Pending', amount: 250 },
  { id: 3, packageName: 'Osaka Adventure', agent: 'Michael Johnson', date: '2023-08-20', status: 'Confirmed', amount: 180 },
];

// Sample booking data for agents
const sampleAgentBookings = [
  { id: 1, packageName: 'Mount Fuji Trek', user: 'Alice Brown', date: '2023-09-05', status: 'Confirmed', amount: 280 },
  { id: 2, packageName: 'Kyoto Experience', user: 'Bob Green', date: '2023-09-10', status: 'Pending', amount: 230 },
  { id: 3, packageName: 'Tokyo City Tour', user: 'Charlie White', date: '2023-09-15', status: 'Confirmed', amount: 150 },
];

function Booking({ isAgent }) {
  const theme = useTheme();
  const [bookings, setBookings] = useState(isAgent ? sampleAgentBookings : sampleUserBookings);
  const [selectedBooking, setSelectedBooking] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isConfirmModalOpen, setIsConfirmModalOpen] = useState(false);

  const isSmallScreen = useMediaQuery(theme.breakpoints.down('sm'));

  const handleCancelBooking = (bookingId) => {
    const updatedBookings = bookings.map((booking) =>
      booking.id === bookingId ? { ...booking, status: 'Canceled' } : booking
    );
    setBookings(updatedBookings);

    // Implement wallet update logic here for user cancellations

    handleCloseConfirmModal();
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
                    <TableCellBold>{isAgent ? 'User' : 'Agent'}</TableCellBold>
                    <TableCellBold>Date</TableCellBold>
                    <TableCellBold>Status</TableCellBold>
                    <TableCellBold>Actions</TableCellBold>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {bookings.map((booking) => (
                    <TableRow key={booking.id}>
                      <TableCellBold>{booking.id}</TableCellBold>
                      <TableCellBold>{booking.packageName}</TableCellBold>
                      <TableCellBold>{isAgent ? booking.user : booking.agent}</TableCellBold>
                      <TableCellBold>{booking.date}</TableCellBold>
                      <TableCellBold>{booking.status}</TableCellBold>
                      <TableCellBold>
                        {booking.status === 'Confirmed' && (
                          <>
                            <Button onClick={() => handleOpenModal(booking.id)}>View</Button>
                            {!isAgent && (
                              <>
                                <Button onClick={() => handleOpenConfirmModal(booking.id)} color="error">
                                  Cancel
                                </Button>
                                <Dialog open={isConfirmModalOpen} onClose={handleCloseConfirmModal}>
                                  <DialogTitle>Confirm Cancellation</DialogTitle>
                                  <DialogContent>
                                    Are you sure you want to cancel this booking?
                                  </DialogContent>
                                  <DialogActions>
                                    <Button onClick={handleCloseConfirmModal} color="primary">
                                      No
                                    </Button>
                                    <ConfirmButton onClick={() => handleCancelBooking(selectedBooking)}>
                                      Yes, Cancel
                                    </ConfirmButton>
                                  </DialogActions>
                                </Dialog>
                              </>
                            )}
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
        <Modal
          open={isModalOpen}
          onClose={handleCloseModal}
          closeAfterTransition
          BackdropComponent={Backdrop}
          BackdropProps={{
            timeout: 500,
          }}
        >
          <Fade in={isModalOpen}>
            <ModalContainer>
              <ModalContent>
                {/* Modal content goes here */}
                {selectedBooking !== null && (
                  <div>
                    <Typography variant="h6">Booking Details</Typography>
                    {/* Display booking details */}
                  </div>
                )}
              </ModalContent>
            </ModalContainer>
          </Fade>
        </Modal>
      </Container>
    </BookingContainer>
  );
}

export default Booking;
