import React from 'react';
import { List, ListItem, ListItemText, Divider, Typography, Avatar } from '@mui/material';
import StarRating from './StarRating'; // Make sure to provide the correct path to the StarRating component

const styles = {
  avatar: {
    backgroundColor: '#1976d2',
  },
};

function ReviewList({ reviews }) {
  return (
    <div>
      <Typography variant="h6">Reviews</Typography>
      <List>
        {reviews.map((review, index) => (
          <div key={index}>
            <ListItem alignItems="flex-start">
              <Avatar style={styles.avatar}>R</Avatar>
              <ListItemText
                primary={review.name}
                secondary={
                  <React.Fragment>
                    <StarRating value={review.rating} />
                    {` - ${review.date}`}
                  </React.Fragment>
                }
              />
            </ListItem>
            <ListItem>
              <ListItemText primary={review.comment} />
            </ListItem>
            <Divider />
          </div>
        ))}
      </List>
    </div>
  );
}

export default ReviewList;
