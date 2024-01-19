import { useState } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'
import { Container, Typography, Button, TextField, Box, Grid } from '@mui/material'

const CaseParticipantPage = ({ caseParticipant }) => {
	const [isEditMode, setIsEditMode] = useState(false)
	const [participant, setParticipant] = useState(caseParticipant || {})

	const handleChange = (e) => {
		setParticipant({ ...participant, [e.target.name]: e.target.value })
	}

	const handleSubmit = async (e) => {
		e.preventDefault()
		try {
			await axios.put(`http://api:8080/v1/CaseParticipants/${participant.caseParticipantEntityId}`, participant)
			alert('Case participant updated successfully!')
			setIsEditMode(false) // Switch back to view mode after update
		} catch (error) {
			console.error('Error updating case participant:', error)
			alert('Failed to update case participant.')
		}
	}

	return (
		<Container>
			<Typography variant="h4" sx={{ mb: 4 }}>Case Participant Detail</Typography>
			{!isEditMode ? (
				<Box>
					{participant ? (
						<>
							<Typography>Name: {participant.caseParticipantFirstName} {participant.caseParticipantMiddleName} {participant.caseParticipantLastName}</Typography>
							<Typography>Type: {participant.caseParticipantType}</Typography>
							<Button variant="contained" color="primary" onClick={() => setIsEditMode(true)} sx={{ mt: 2 }}>Edit</Button>
						</>
					) : (
						<Typography>Case participant detail not found.</Typography>
					)}
				</Box>
			) : (
				<form onSubmit={handleSubmit}>
					<Grid container spacing={2}>
						<Grid item xs={12}>
							<TextField
								label="First Name"
								name="caseParticipantFirstName"
								value={participant.caseParticipantFirstName}
								onChange={handleChange}
								fullWidth
							/>
						</Grid>
						<Grid item xs={12}>
							<TextField
								label="Middle Name"
								name="caseParticipantMiddleName"
								value={participant.caseParticipantMiddleName}
								onChange={handleChange}
								fullWidth
							/>
						</Grid>
						<Grid item xs={12}>
							<TextField
								label="Last Name"
								name="caseParticipantLastName"
								value={participant.caseParticipantLastName}
								onChange={handleChange}
								fullWidth
							/>
						</Grid>
						{/* Other fields as required */}
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
		const res = await axios.get(`http://api:8080/v1/CaseParticipants/${id}`)
		const caseParticipant = res.data // Adjust this according to the API response

		return {
			props: { caseParticipant }
		}
	} catch (error) {
		console.error('Error fetching case participant detail:', error)
		return {
			props: { caseParticipant: null }
		}
	}
}

CaseParticipantPage.propTypes = {
	caseParticipant: PropTypes.shape({
		caseParticipantEntityId: PropTypes.string.isRequired,
		caseParticipantType: PropTypes.number.isRequired,
		caseParticipantFirstName: PropTypes.string.isRequired,
		caseParticipantLastName: PropTypes.string.isRequired,
		caseParticipantMiddleName: PropTypes.string
	})
}

export default CaseParticipantPage
