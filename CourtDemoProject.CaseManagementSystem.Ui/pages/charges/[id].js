import { useState } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'
import { Container, Typography, Button, TextField, Box, Grid, FormControl, InputLabel, Select, MenuItem } from '@mui/material'

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
					<Typography variant="h6">Charge Name: {editedCharge.chargeName}</Typography>
					<Typography variant="h6">Charge Code: {editedCharge.chargeCode}</Typography>
					<Typography variant="h6">Charge Type: {editedCharge.chargeType}</Typography>
					<Typography variant="h6">Fine Amount: {editedCharge.fineAmount}</Typography>
					<Typography variant="h6">Sentence Length (days): {editedCharge.sentenceLengthIndays}</Typography>
					<Button variant="contained" color="primary" onClick={() => setIsEditMode(true)} sx={{ mt: 2 }}>Edit</Button>
				</Box>
			) : (
				<form onSubmit={handleSubmit}>
					<Grid container spacing={2}>
						<Grid item xs={12}>
							<TextField
								label="Charge Name"
								name="chargeName"
								value={editedCharge.chargeName}
								onChange={handleChange}
								fullWidth
							/>
						</Grid>
						<Grid item xs={12}>
							<TextField
								label="Charge Code"
								name="chargeCode"
								value={editedCharge.chargeCode}
								onChange={handleChange}
								fullWidth
							/>
						</Grid>
						<Grid item xs={12}>
							<FormControl fullWidth>
								<InputLabel id="chargeType-label">Charge Type</InputLabel>
								<Select
									labelId="chargeType-label"
									id="chargeType"
									name="chargeType"
									value={editedCharge.chargeType}
									label="Charge Type"
									onChange={handleChange}
								>
									{/* Populate the options dynamically based on ChargeTypeEnum */}
									{/* ... */}
								</Select>
							</FormControl>
						</Grid>
						<Grid item xs={12}>
							<TextField
								label="Fine Amount"
								name="fineAmount"
								type="number"
								value={editedCharge.fineAmount}
								onChange={handleChange}
								fullWidth
							/>
						</Grid>
						<Grid item xs={12}>
							<TextField
								label="Sentence Length (days)"
								name="sentenceLengthIndays"
								type="number"
								value={editedCharge.sentenceLengthIndays}
								onChange={handleChange}
								fullWidth
							/>
						</Grid>
						{/* Add more fields if required */ }
					</Grid >
					<Box sx={{ mt: 2 }}>
						<Button variant="contained" color="primary" type="submit">Update</Button>
						<Button variant="outlined" onClick={() => setIsEditMode(false)} sx={{ ml: 2 }}>Cancel</Button>
					</Box>
				</form >
			)}
		</Container >
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
