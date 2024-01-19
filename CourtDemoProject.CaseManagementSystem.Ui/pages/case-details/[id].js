import { useState } from 'react'
import styles from '../../styles/pages/case-details/Detail.module.scss'
import PropTypes from 'prop-types'
import axios from 'axios'
import Link from 'next/link'
import { Container, Typography, Button, TextField, Box, Grid } from '@mui/material'

const CaseDetailPage = ({ caseDetail }) => {
	const [isEditMode, setIsEditMode] = useState(false)
	const [detail, setDetail] = useState(caseDetail || {})

	const handleChange = (e) => {
		setDetail({ ...detail, [e.target.name]: e.target.value })
	}

	const handleSubmit = async (e) => {
		e.preventDefault()
		try {
			await axios.put(
				`http://api:8080/v1/CaseDetails/${detail.caseDetailId}`,
				detail
			)
			alert('Case detail updated successfully!')
			setIsEditMode(false) // Switch back to view mode after update
		} catch (error) {
			console.error('Error updating case detail:', error)
			alert('Failed to update case detail.')
		}
	}

	return (
		<Container>
			<Typography variant="h4" sx={{ mb: 4 }}>Case Detail</Typography>
			{!isEditMode ? (
				<Box>
					{detail ? (
						<>
							<Typography>Date: {new Date(detail.caseDetailEntryDateTime).toLocaleDateString()}</Typography>
							<Typography>Description: {detail.description}</Typography>
							<Typography>Docket Detail: {detail.docketDetail}</Typography>
							{detail.documentUri && <Typography>Document: <a href={detail.documentUri}>{detail.documentUri}</a></Typography>}
							<Button variant="contained" color="primary" onClick={() => setIsEditMode(true)} sx={{ mt: 2 }}>Edit</Button>
						</>
					) : (
						<Typography>Case detail not found.</Typography>
					)}
				</Box>
			) : (
				<form onSubmit={handleSubmit}>
					{/* Add TextField components for form fields */}
					{/* ... */}
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
		const res = await axios.get(`http://api:8080/v1/CaseDetails/${id}`)
		const caseDetail = res.data // Adjust this according to the API response

		return {
			props: { caseDetail }
		}
	} catch (error) {
		console.error('Error fetching case detail:', error)
		return {
			props: { caseDetail: null }
		}
	}
}

CaseDetailPage.propTypes = {
	caseDetail: PropTypes.shape({
		caseDetailId: PropTypes.string.isRequired,
		caseDetailEntryDateTime: PropTypes.string.isRequired,
		description: PropTypes.string,
		docketDetail: PropTypes.string,
		documentUri: PropTypes.string
	})
}

export default CaseDetailPage
