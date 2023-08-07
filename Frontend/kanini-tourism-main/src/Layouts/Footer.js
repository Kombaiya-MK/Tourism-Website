import React from 'react';
import { Grid, Typography, Button } from '@mui/material';
import { styled } from '@mui/system';
import FacebookIcon from '@mui/icons-material/Facebook';
import TwitterIcon from '@mui/icons-material/Twitter';
import LinkedInIcon from '@mui/icons-material/LinkedIn';
import EmailIcon from '@mui/icons-material/Email';

const StyledFooter = styled('footer')({
  backgroundColor: '#f7f7f7', // Light white color
  padding: '2rem',
});

const Footer = () => (
  <StyledFooter>
    <Grid container spacing={3} justifyContent="center">
      <Grid item>
        <a href="#">
          <FacebookIcon />
        </a>
      </Grid>
      <Grid item>
        <a href="#">
          <TwitterIcon />
        </a>
      </Grid>
      <Grid item>
        <a href="#">
          <LinkedInIcon />
        </a>
      </Grid>
      <Grid item>
        <a href="#">
          <EmailIcon />
        </a>
      </Grid>
    </Grid>
    <form style={{ marginTop: '2rem' }}>
      <Grid container spacing={2} justifyContent="center">
        <Grid item>
          <Typography variant="body2" style={{ fontWeight: 'bold', color: 'black' }}>
            Sign up for our newsletter
          </Typography>
        </Grid>
        <Grid item>
          <input
            type="email"
            id="form5Example21"
            className="form-control"
            style={{ backgroundColor: 'white' }}
          />
          <label htmlFor="form5Example21">Email address</label>
        </Grid>
        <Grid item>
          <Button variant="outlined" style={{ color: 'black', borderColor: 'black' }}>
            Subscribe
          </Button>
        </Grid>
      </Grid>
    </form>
    <Typography variant="body2" style={{ marginTop: '2rem', color: 'black' }}>
      Experience the beauty of Japan with Travel Japan! Our team is dedicated to showcasing the diverse culture, stunning landscapes, and rich history of Japan. Discover unforgettable destinations, explore local traditions, and indulge in the finest cuisine that Japan has to offer. Whether you're an avid traveler or planning your first trip, let Travel Japan be your guide to an extraordinary journey.
    </Typography>
    <div className="text-center p-3" style={{ backgroundColor: 'rgba(0, 0, 0)' }}>
      © {new Date().getFullYear()} traveljapan.com
    </div>
  </StyledFooter>
);

export default Footer;
