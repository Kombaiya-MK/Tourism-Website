import React from 'react';
import { Rating, Box } from '@mui/material';

function StarRating({ value }) {
  return (
    <Box component="fieldset" borderColor="transparent">
      <Rating
        name="star-rating"
        value={value}
        precision={0.5}
        readOnly
      />
    </Box>
  );
}

export default StarRating;
