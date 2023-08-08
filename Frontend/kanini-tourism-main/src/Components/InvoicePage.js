import React, { useEffect, useState } from 'react';
import {
  Typography,
  Paper,
  Table,
  TableContainer,
  TableHead,
  TableBody,
  TableRow,
  TableCell,
  Button,
  Tooltip,
} from '@mui/material';
import { Print as PrintIcon, ArrowBack as ArrowBackIcon, GetApp as GetAppIcon } from '@mui/icons-material';
import jsPDF from 'jspdf';
import 'jspdf-autotable';
import axios from 'axios';

const InvoicePage = ({ selectedBookingId, history }) => {
  const [selectedBooking, setSelectedBooking] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchBooking = async () => {
      try {
        const response = await axios.get(`http://localhost:5066/api/Booking/GetBooking/${selectedBookingId}`);
        setSelectedBooking(response.data);
        setIsLoading(false);
      } catch (error) {
        console.error('Error fetching booking:', error);
        setIsLoading(false);
      }
    };

    fetchBooking();
  }, [selectedBookingId]);

  const handlePrint = () => {
    if (!selectedBooking) return;

    const doc = new jsPDF();
    doc.text('Invoice', 10, 10);
    doc.autoTable({
      head: [['Package', 'No of Customers', 'Adults', 'Children']],
      body: [[selectedBooking.packageName, selectedBooking.noofAdults + selectedBooking.noofChildren, selectedBooking.noofAdults, selectedBooking.noofChildren]],
      startY: 20,
    });
    doc.text(`Total Amount: $${selectedBooking.price}`, 10, doc.autoTable.previous.finalY + 10);
    doc.save('invoice.pdf');
  };

  return (
    <div className="invoice-container">
      <Paper elevation={3} className="invoice-paper">
        <Typography variant="h4" gutterBottom>
          Invoice
        </Typography>
        {isLoading ? (
          <Typography>Loading...</Typography>
        ) : (
          selectedBooking && (
            <>
              <TableContainer>
                <Table>
                  <TableHead>
                    <TableRow>
                      <TableCell>Package</TableCell>
                      <TableCell>No of Customers</TableCell>
                      <TableCell>Adults</TableCell>
                      <TableCell>Children</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    <TableRow>
                      <TableCell>{selectedBooking.packageName}</TableCell>
                      <TableCell>{selectedBooking.noofAdults + selectedBooking.noofChildren}</TableCell>
                      <TableCell>{selectedBooking.noofAdults}</TableCell>
                      <TableCell>{selectedBooking.noofChildren}</TableCell>
                    </TableRow>
                  </TableBody>
                </Table>
              </TableContainer>
              <div className="total-amount">
                <Typography variant="h6">Total Amount: ${selectedBooking.price}</Typography>
              </div>
              <div className="invoice-buttons">
                <Tooltip title="Download Invoice PDF">
                  <Button variant="outlined" startIcon={<GetAppIcon />} onClick={handlePrint}>
                    Download
                  </Button>
                </Tooltip>
                <Tooltip title="Back to Cart">
                  <Button variant="outlined" startIcon={<ArrowBackIcon />} onClick={() => history.push('/cart')}>
                    Back to Cart
                  </Button>
                </Tooltip>
                {selectedBooking.status === 'Confirmed' && (
                  <Tooltip title="Cancel Booking">
                    <Button variant="outlined" color="error" onClick={() => handleCancelBooking(selectedBooking.bookingId)}>
                      Cancel Booking
                    </Button>
                  </Tooltip>
                )}
                <Tooltip title="Print Invoice">
                  <Button variant="outlined" startIcon={<PrintIcon />} onClick={handlePrint}>
                    Print
                  </Button>
                </Tooltip>
              </div>
            </>
          )
        )}
      </Paper>
    </div>
  );
};

export default InvoicePage;
