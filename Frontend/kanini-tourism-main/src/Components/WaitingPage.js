import React from 'react';
import { Container, Typography, CircularProgress } from '@mui/material';
import { styled } from '@mui/system';

const WaitingContainer = styled('div')({
  paddingTop: '2rem',
  paddingBottom: '2rem',
  textAlign: 'center',
});

function WaitingPage() {
  return (
    <WaitingContainer>
      <Container>
        <CircularProgress size={100} />
        <Typography variant="h2" gutterBottom>
          Waiting for Approval
        </Typography>
        <Typography variant="body1" paragraph>
          Your request is pending approval from the agent. You will be notified once it's approved.
        </Typography>
      </Container>
    </WaitingContainer>
  );
}

export default WaitingPage;
