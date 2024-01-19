import { useState } from 'react'
import axios from 'axios'
import { useRouter } from 'next/router'
import { Container, Typography, TextField, FormControl, InputLabel, Select, MenuItem, Button, Box } from '@mui/material'

const CreateCasePage = () => {
	const [caseData, setCaseData] = useState({
		courtName: '',
		caseType: '', // Enum value
		dateOfOffense: '', // Date
		verdict: '', // Enum value
		plea: '', // Enum value
		courtDates: [], // Array of dates
		caseStatus: '' // Enum value
	})
	const router = useRouter()

	const handleChange = (e) => {
		if (e.target.name === 'courtDates') {
			// Assuming multiple date inputs are managed separately
			setCaseData({ ...caseData, courtDates: [...caseData.courtDates, e.target.value] })
		} else {
			setCaseData({ ...caseData, [e.target.name]: e.target.value })
		}
	}

	const handleAddCourtDate = () => {
		setCaseData({ ...caseData, courtDates: [...caseData.courtDates, ''] })
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

				<FormControl fullWidth margin="normal">
					<TextField
						label="Court Name"
						id="courtName"
						name="courtName"
						value={caseData.courtName}
						onChange={handleChange}
						required
					/>
				</FormControl>

				<FormControl fullWidth margin="normal">
					<InputLabel id="caseType-label">Case Type</InputLabel>
					<Select
						labelId="caseType-label"
						id="caseType"
						name="caseType"
						value={caseData.caseType}
						label="Case Type"
						onChange={handleChange}
					>
						<MenuItem value="0">Criminal</MenuItem>
						<MenuItem value="1">Civil</MenuItem>
						<MenuItem value="2">Family</MenuItem>
						<MenuItem value="3">Juvenile</MenuItem>
						<MenuItem value="4">Probate</MenuItem>
						<MenuItem value="5">SmallClaims</MenuItem>
					</Select>
				</FormControl>

				<FormControl fullWidth margin="normal">
					<TextField
						id="dateOfOffense"
						name="dateOfOffense"
						label="Date Of Offense"
						type="date"
						value={caseData.dateOfOffense}
						onChange={handleChange}
						InputLabelProps={{ shrink: true }}
					/>
				</FormControl>

				<FormControl fullWidth margin="normal">
					<InputLabel id="verdict-label">Verdict</InputLabel>
					<Select
						labelId="verdict-label"
						id="verdict"
						name="verdict"
						value={caseData.verdict}
						label="Verdict"
						onChange={handleChange}
					>
						<MenuItem value="0">Guilty</MenuItem>
						<MenuItem value="1">Not Guilty</MenuItem>
						<MenuItem value="2">In Progress</MenuItem>
					</Select>
				</FormControl>

				<FormControl fullWidth margin="normal">
					<InputLabel id="plea-label">Plea</InputLabel>
					<Select
						labelId="plea-label"
						id="plea"
						name="plea"
						value={caseData.plea}
						label="Plea"
						onChange={handleChange}
					>
						<MenuItem value="0">Guilty</MenuItem>
						<MenuItem value="1">Not Guilty</MenuItem>
						<MenuItem value="2">No Contest</MenuItem>
						<MenuItem value="3">Alford Plea</MenuItem>
						<MenuItem value="4">Refusal To Plea</MenuItem>
					</Select>
				</FormControl>

				{caseData.courtDates.map((courtDate, index) => (
					<FormControl fullWidth margin="normal" key={index}>
						<TextField
							id={`courtDate-${index}`}
							name="courtDates"
							label={`Court Date ${index + 1}`}
							type="datetime-local"
							value={courtDate}
							onChange={(e) => handleChange(e, index)}
							InputLabelProps={{ shrink: true }}
						/>
					</FormControl>
				))}
				<Button variant="outlined" onClick={handleAddCourtDate} sx={{ mb: 2 }}>
					Add Court Date
				</Button>

				<FormControl fullWidth margin="normal">
					<InputLabel id="caseStatus-label">Case Status</InputLabel>
					<Select
						labelId="caseStatus-label"
						id="caseStatus"
						name="caseStatus"
						value={caseData.caseStatus}
						label="Case Status"
						onChange={handleChange}
					>
						<MenuItem value="0">Open</MenuItem>
						<MenuItem value="1">Closed</MenuItem>
						<MenuItem value="2">In Progress</MenuItem>
					</Select>
				</FormControl>

				<Box sx={{ mt: 2 }}>
					<Button type="submit" variant="contained" color="primary">Create Case</Button>
				</Box>
			</form>
		</Container>
	)
}

export default CreateCasePage
