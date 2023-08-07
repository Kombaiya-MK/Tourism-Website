import React, { useState, useEffect } from 'react';
import {
  Typography,
  Paper,
  Tabs,
  Tab,
  TableContainer,
  Table,
  TableHead,
  TableBody,
  TableRow,
  TableCell,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  Tooltip,
} from '@mui/material';
import CheckIcon from '@mui/icons-material/Check';
import ClearIcon from '@mui/icons-material/Clear';
import axios from 'axios';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const AgencyApproval = () => {
  const [selectedTab, setSelectedTab] = useState(0);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedAgency, setSelectedAgency] = useState(null);
  const [agencyApprovals, setAgencyApprovals] = useState([]);
  const [registeredAgencies, setRegisteredAgencies] = useState([]);

  useEffect(() => {
    // Fetch agency approvals and registered agencies data
    axios.get('https://localhost:7289/api/User/GetAllTravelAgents')
      .then(response => {
        const approvals = response.data.filter(agent => agent.travelAgent.length === 0);
        const registered = response.data.filter(agent => agent.travelAgent.length > 0);
        setAgencyApprovals(approvals);
        setRegisteredAgencies(registered);
      })
      .catch(error => {
        console.error('Error fetching agency data:', error);
      });
  }, []);

  const handleTabChange = (event, newValue) => {
    setSelectedTab(newValue);
  };

  const handleApprove = (agencyId) => {
    // Logic to approve the travel agency with the given agencyId
    console.log(`Approving agency with ID: ${agencyId}`);
    toast.success('Agency approved successfully');
  };

  const handleDisapprove = (agencyId) => {
    // Logic to disapprove the travel agency with the given agencyId
    console.log(`Disapproving agency with ID: ${agencyId}`);
    toast.error('Agency disapproved');
  };

  const handleModalOpen = (agency) => {
    setSelectedAgency(agency);
    setIsModalOpen(true);
  };

  const handleModalClose = () => {
    setSelectedAgency(null);
    setIsModalOpen(false);
  };

  return (
    <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', margin: '40px auto', maxWidth: 800 }}>
      <Paper elevation={3} style={{ padding: 20, width: '100%' }}>
        <Typography variant="h4" gutterBottom>
          Agency Approvals and Registered Agencies
        </Typography>
        <Tabs value={selectedTab} onChange={handleTabChange}>
          <Tab label="Approvals" />
          <Tab label="Registered Agencies" />
        </Tabs>
        <div style={{ marginTop: 20 }}>
          <TableContainer>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell>ID</TableCell>
                  <TableCell>Name</TableCell>
                  <TableCell>Status</TableCell>
                  <TableCell>Actions</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {selectedTab === 0 && agencyApprovals.map(agency => (
                  <TableRow key={agency.id}>
                    <TableCell>{agency.id}</TableCell>
                    <TableCell>{agency.userDetails[0]?.userName}</TableCell>
                    <TableCell>Pending</TableCell>
                    <TableCell>
                      <Tooltip title="View Agency">
                        <Button variant="outlined" color="primary" onClick={() => handleModalOpen(agency)}>View</Button>
                      </Tooltip>
                    </TableCell>
                  </TableRow>
                ))}
                {selectedTab === 1 && registeredAgencies.map(agency => (
                  <TableRow key={agency.id}>
                    <TableCell>{agency.id}</TableCell>
                    <TableCell>{agency.userDetails[0]?.userName}</TableCell>
                    <TableCell>Approved</TableCell>
                    <TableCell>
                      <Tooltip title="View Agency">
                        <Button variant="outlined" color="primary" onClick={() => handleModalOpen(agency)}>View</Button>
                      </Tooltip>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </div>
      </Paper>

      <Dialog open={isModalOpen} onClose={handleModalClose}>
        <DialogTitle>{selectedAgency && selectedAgency.userDetails[0]?.userName}</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Status: {selectedAgency && (selectedAgency.travelAgent.length > 0 ? 'Approved' : 'Pending')}
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleModalClose} color="primary">
            Close
          </Button>
          {selectedAgency && selectedTab === 0 && (
            <>
              <Tooltip title="Approve Agency">
                <Button
                  startIcon={<CheckIcon />}
                  onClick={() => handleApprove(selectedAgency.id)}
                  color="primary"
                >
                  Approve
                </Button>
              </Tooltip>
              <Tooltip title="Disapprove Agency">
                <Button
                  startIcon={<ClearIcon />}
                  onClick={() => handleDisapprove(selectedAgency.id)}
                  color="secondary"
                >
                  Disapprove
                </Button>
              </Tooltip>
            </>
          )}
        </DialogActions>
      </Dialog>
    </div>
  );
};

export default AgencyApproval;
