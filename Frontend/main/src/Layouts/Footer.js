import React from 'react';
import { Grid, Typography, Button } from '@mui/material';
import FacebookIcon from '@mui/icons-material/Facebook';
import TwitterIcon from '@mui/icons-material/Twitter';
import LinkedInIcon from '@mui/icons-material/LinkedIn';
import EmailIcon from '@mui/icons-material/Email';

const Footer = () => (
  <footer style={{ backgroundColor: 'black' }}>
    <div style={{ padding: '2rem' }}>
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
            <Typography variant="body2" style={{ fontWeight: 'bold' }}>
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
            <Button variant="outlined" style={{ color: 'white', borderColor: 'white' }}>
              Subscribe
            </Button>
          </Grid>
        </Grid>
      </form>
      <Typography variant="body2" style={{ marginTop: '2rem', color: 'white' }}>
        OtakuKulture pushes the envelope of Japanese cuisine. Taking its influences from our team members’ culinary roots, OtakuKulture blends traditional and innovative techniques to create unique offerings using local ingredients in all of its dishes. OtakuKulture is grounded in hospitality with our staff’s commitment to providing you with a memorable experience each time you walk through our door.
      </Typography>
    </div>
    <div className="text-center p-3" style={{ backgroundColor: 'rgba(0, 0, 0)' }}>
      © {new Date().getFullYear()} otakukulture.in
    </div>
  </footer>
);

export default Footer;
