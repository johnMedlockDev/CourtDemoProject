import { useState } from 'react'
import axios from 'axios'
import { useRouter } from 'next/router'
import { Container, TextField, FormControl, InputLabel, Select, MenuItem, Button, Typography } from '@mui/material'

const CreateCasePage = () => {
	const [caseData, setCaseData] = useState({
		courtName: '',
		caseType: '' // Use select dropdown for case types
	})
	const router = useRouter()

	const handleChange = (e) => {
		setCaseData({ ...caseData, [e.target.name]: e.target.value })
	}

	const handleSubmit = async (e) => {
		e.preventDefault()
		try {
			await axios.post('http://api:8080/v1/Cases', caseData)
			router.push('/cases')
		} catch (error) {
			console.error('Error creating case:', error)
		}
	}

	return (
		<Container>
			<Typography variant="h4" sx={{ mb: 2 }}>Create New Case</Typography>
			<form onSubmit={handleSubmit}>
				<TextField
					label="Court Name"
					id="courtName"
					name="courtName"
					value={caseData.courtName}
					onChange={handleChange}
					fullWidth
					margin="normal"
					required
				/>
				<FormControl fullWidth margin="normal">
					<InputLabel id="caseType-label">Case Type</InputLabel>
					<Select
						labelId="caseType-label"
						id="caseType"
						name="caseType"
						value={caseData.caseType}
						onChange={handleChange}
						label="Case Type"
						required
					>
						<MenuItem value=""><em>None</em></MenuItem>
						{/* Populate the options dynamically based on your case types */}
						<MenuItem value="type1">Type 1</MenuItem>
						<MenuItem value="type2">Type 2</MenuItem>
						{/* ... other case types ... */}
					</Select>
				</FormControl>
				{/* Add other input fields as needed */}
				<Button type="submit" variant="contained" color="primary" sx={{ mt: 2 }}>
                    Create Case
				</Button>
			</form>
		</Container>
	)
}

export default CreateCasePage
