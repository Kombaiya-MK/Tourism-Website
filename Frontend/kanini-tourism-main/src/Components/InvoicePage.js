import React from 'react';
import '../Assets/Styles/Invoice.css'
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

const InvoicePage = ({ selectedPacks, totalAmount, history }) => {
  const handlePrint = () => {
    const doc = new jsPDF();
    doc.text('Invoice', 10, 10);
    doc.autoTable({
      head: [['Package', 'No of Customers', 'Adults', 'Children']],
      body: selectedPacks.map(pack => [pack.name, pack.customers, pack.adults, pack.children]),
      startY: 20,
    });
    doc.text(`Total Amount: $${totalAmount}`, 10, doc.autoTable.previous.finalY + 10);
    doc.save('invoice.pdf');
  };

  return (
    <div className="invoice-container">
      <Paper elevation={3} className="invoice-paper">
        <Typography variant="h4" gutterBottom>
          Invoice
        </Typography>
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
              {selectedPacks.map(pack => (
                <TableRow key={pack.id}>
                  <TableCell>{pack.name}</TableCell>
                  <TableCell>{pack.customers}</TableCell>
                  <TableCell>{pack.adults}</TableCell>
                  <TableCell>{pack.children}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
        <div className="total-amount">
          <Typography variant="h6">Total Amount: ${totalAmount}</Typography>
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
          <Tooltip title="Print Invoice">
            <Button variant="outlined" startIcon={<PrintIcon />} onClick={handlePrint}>
              Print
            </Button>
          </Tooltip>
        </div>
      </Paper>
    </div>
  );
};

export default InvoicePage;
