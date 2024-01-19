import { useState } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'
import { Container, Typography, Button, TextField, Box, Grid, FormControl, InputLabel, Select, MenuItem } from '@mui/material'

const CasePage = ({ caseItem }) => {
	const [isEditMode, setIsEditMode] = useState(false)
	const [editedCase, setEditedCase] = useState(caseItem || {})

	const handleChange = (e) => {
		setEditedCase({ ...editedCase, [e.target.name]: e.target.value })
	}

	const handleSubmit = async (e) => {
		e.preventDefault()
		try {
			await axios.put(`http://api:8080/v1/Cases/${editedCase.caseId}`, editedCase)
			alert('Case updated successfully!')
			setIsEditMode(false) // Switch back to view mode after update
		} catch (error) {
			console.error('Error updating case:', error)
			alert('Failed to update case.')
		}
	}

	return (
		<Container>
			<Typography variant="h4" sx={{ mb: 4 }}>Case</Typography>
			{!isEditMode ? (
				<Box>
					<Typography variant="h6">Court Name: {editedCase.courtName}</Typography>
					<Typography variant="h6">Case Type: {editedCase.caseType}</Typography>
					{/* Display other case details here */}
					{/* ... */}
					<Button variant="contained" color="primary" onClick={() => setIsEditMode(true)} sx={{ mt: 2 }}>Edit</Button>
				</Box>
			) : (
				<form onSubmit={handleSubmit}>
					<Grid container spacing={2}>
						<Grid item xs={12}>
							<TextField
								label="Court Name"
								name="courtName"
								value={editedCase.courtName}
								onChange={handleChange}
								fullWidth
							/>
						</Grid>
						<Grid item xs={12}>
							<FormControl fullWidth>
								<InputLabel id="caseType-label">Case Type</InputLabel>
								<Select
									labelId="caseType-label"
									id="caseType"
									name="caseType"
									value={editedCase.caseType}
									label="Case Type"
									onChange={handleChange}
								>
									{/* Populate the options dynamically based on CaseTypeEnum */}
									{/* ... */}
								</Select>
							</FormControl>
						</Grid>
						{/* Additional fields for DateOfOffense, Verdict, Plea, CaseStatus */ }
						{/* ... */ }
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
		const res = await axios.get(`http://api:8080/v1/Cases/${id}`)
		const caseItem = res.data // Adjust this according to the API response

		return {
			props: { caseItem }
		}
	} catch (error) {
		console.error('Error fetching case detail:', error)
		return {
			props: { caseItem: null }
		}
	}
}

CasePage.propTypes = {
	caseItem: PropTypes.shape({
		caseId: PropTypes.string.isRequired,
		courtName: PropTypes.string,
		caseType: PropTypes.number.isRequired
		// Include other properties as required
	})
}

export default CasePage
