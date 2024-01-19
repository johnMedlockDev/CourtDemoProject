import { useState } from 'react'
import axios from 'axios'
import { useRouter } from 'next/router'
import { Container, Typography, TextField, FormControl, InputLabel, Select, MenuItem, Button } from '@mui/material'

const CreateCaseParticipantPage = () => {
	const [participantData, setParticipantData] = useState({
		caseParticipantFirstName: '',
		caseParticipantMiddleName: '',
		caseParticipantLastName: '',
		caseParticipantType: ''
	})
	const router = useRouter()

	const handleChange = (e) => {
		setParticipantData({ ...participantData, [e.target.name]: e.target.value })
	}

	const handleSubmit = async (e) => {
		e.preventDefault()
		try {
			await axios.post('http://api:8080/v1/CaseParticipants', participantData)
			router.push('/case-participants')
		} catch (error) {
			console.error('Error creating case participant:', error)
		}
	}

	return (
		<Container>
			<Typography variant="h4" sx={{ mb: 2 }}>Create New Case Participant</Typography>
			<form onSubmit={handleSubmit}>
				<TextField
					label="First Name"
					id="firstName"
					name="caseParticipantFirstName"
					value={participantData.caseParticipantFirstName}
					onChange={handleChange}
					required
					fullWidth
					margin="normal"
				/>
				<TextField
					label="Middle Name"
					id="middleName"
					name="caseParticipantMiddleName"
					value={participantData.caseParticipantMiddleName}
					onChange={handleChange}
					fullWidth
					margin="normal"
				/>
				<TextField
					label="Last Name"
					id="lastName"
					name="caseParticipantLastName"
					value={participantData.caseParticipantLastName}
					onChange={handleChange}
					required
					fullWidth
					margin="normal"
				/>
				<FormControl fullWidth margin="normal">
					<InputLabel id="participantType-label">Participant Type</InputLabel>
					<Select
						labelId="participantType-label"
						id="participantType"
						name="caseParticipantType"
						value={participantData.caseParticipantType}
						label="Participant Type"
						onChange={handleChange}
						required
					>
						<MenuItem value="0">Judge</MenuItem>
						<MenuItem value="1">Plaintiff</MenuItem>
						<MenuItem value="2">Defendant</MenuItem>
						<MenuItem value="3">Attorney</MenuItem>
						<MenuItem value="4">Witness</MenuItem>
					</Select>
				</FormControl>
				<Button type="submit" variant="contained" color="primary" sx={{ mt: 2 }}>
                    Create Participant
				</Button>
			</form>
		</Container>
	)
}

export default CreateCaseParticipantPage
