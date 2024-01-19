import { useState } from 'react'
import axios from 'axios'
import { useRouter } from 'next/router'
import { Container, Typography, TextField, Button, Box } from '@mui/material'

const CreateCaseDetailPage = () => {
	const [detailData, setDetailData] = useState({
		description: '',
		docketDetail: '',
		documentUri: '' // Assuming this is a URL
		// Add other fields as needed, such as dates
	})
	const router = useRouter()

	const handleChange = (e) => {
		setDetailData({ ...detailData, [e.target.name]: e.target.value })
	}

	const handleSubmit = async (e) => {
		e.preventDefault()
		try {
			await axios.post('http://api:8080/v1/CaseDetails', detailData)
			router.push('/case-details')
		} catch (error) {
			console.error('Error creating case detail:', error)
		}
	}

	return (
		<Container>
			<Typography variant="h4" sx={{ mb: 4 }}>Create New Case Detail</Typography>
			<form onSubmit={handleSubmit}>
				<Box sx={{ mb: 2 }}>
					<TextField
						label="Description"
						variant="outlined"
						fullWidth
						name="description"
						value={detailData.description}
						onChange={handleChange}
						required
					/>
				</Box>
				<Box sx={{ mb: 2 }}>
					<TextField
						label="Docket Detail"
						variant="outlined"
						fullWidth
						name="docketDetail"
						value={detailData.docketDetail}
						onChange={handleChange}
						required
					/>
				</Box>
				<Box sx={{ mb: 2 }}>
					<TextField
						label="Document URI"
						variant="outlined"
						fullWidth
						name="documentUri"
						value={detailData.documentUri}
						onChange={handleChange}
						type="url"
					/>
				</Box>
				{/* Add other input fields as needed */}
				<Button variant="contained" color="primary" type="submit">
                    Create Detail
				</Button>
			</form>
		</Container>
	)
}

export default CreateCaseDetailPage
