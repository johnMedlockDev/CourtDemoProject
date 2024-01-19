import { useState } from 'react'
import styles from '../../styles/pages/charges/Charge.module.scss'
import PropTypes from 'prop-types'
import axios from 'axios'
import { Container, Typography, Button, TextField, Box, Grid } from '@mui/material'

const ChargePage = ({ charge }) => {
	const [isEditMode, setIsEditMode] = useState(false)
	const [editedCharge, setEditedCharge] = useState(charge || {})

	const handleChange = (e) => {
		setEditedCharge({ ...editedCharge, [e.target.name]: e.target.value })
	}

	const handleSubmit = async (e) => {
		e.preventDefault()
		try {
			await axios.put(`http://api:8080/v1/Charges/${editedCharge.chargeId}`, editedCharge)
			alert('Charge updated successfully!')
			setIsEditMode(false) // Switch back to view mode after update
		} catch (error) {
			console.error('Error updating charge:', error)
			alert('Failed to update charge.')
		}
	}

	return (
		<Container>
			<Typography variant="h4" sx={{ mb: 4 }}>Charge Detail</Typography>
			{!isEditMode ? (
				<Box>
					{editedCharge ? (
						<>
							<Typography>Name: {editedCharge.chargeName}</Typography>
							<Typography>Code: {editedCharge.chargeCode}</Typography>
							<Typography>Type: {editedCharge.chargeType}</Typography>
							<Typography>Judgement Type: {editedCharge.judgementType}</Typography>
							<Typography>Fine Amount: {editedCharge.fineAmount}</Typography>
							<Typography>Sentence Length (days): {editedCharge.sentenceLengthIndays}</Typography>
							<Button variant="contained" color="primary" onClick={() => setIsEditMode(true)} sx={{ mt: 2 }}>Edit</Button>
						</>
					) : (
						<Typography>Charge detail not found.</Typography>
					)}
				</Box>
			) : (
				<form onSubmit={handleSubmit}>
					{/* Form fields for editing */}
					<Grid container spacing={2}>
						{/* Replace with appropriate TextField fields */}
						{/* ... */}
					</Grid>
					<Box sx={{ mt: 2 }}>
						<Button variant="contained" color="primary" type="submit">Update</Button>
						<Button variant="outlined" onClick={() => setIsEditMode(false)} sx={{ ml: 2 }}>Cancel</Button>
					</Box>
				</form>
			)}
		</Container>
	)
}

export const getServerSideProps = async (context) => {
	const { id } = context.params

	try {
		const res = await axios.get(`http://api:8080/v1/Charges/${id}`)
		const charge = res.data // Adjust this according to the API response

		return {
			props: { charge }
		}
	} catch (error) {
		console.error('Error fetching charge detail:', error)
		return {
			props: { charge: null }
		}
	}
}

ChargePage.propTypes = {
	charge: PropTypes.shape({
		chargeId: PropTypes.string.isRequired,
		chargeName: PropTypes.string.isRequired,
		chargeCode: PropTypes.string.isRequired,
		chargeType: PropTypes.number.isRequired,
		judgementType: PropTypes.number.isRequired,
		fineAmount: PropTypes.number.isRequired,
		sentenceLengthIndays: PropTypes.number.isRequired
	})
}

export default ChargePage
