import PropTypes from 'prop-types'
import axios from 'axios'
import { useRouter } from 'next/router'
import { Container, Typography, List, ListItem, ListItemText, Button, IconButton, Link as MuiLink } from '@mui/material'
import DeleteIcon from '@mui/icons-material/Delete'
import NextLink from 'next/link'

const CaseDetailsPage = ({ caseDetails }) => {
	const router = useRouter()

	const handleDelete = async (caseId) => {
		try {
			await axios.delete(`http://api:8080/v1/CaseDetails/${caseId}`)
			router.replace(router.asPath) // Refresh the page to update the list
		} catch (error) {
			console.error('Error deleting case detail:', error)
		}
	}

	return (
		<Container>
			<Typography variant="h4" sx={{ mb: 2 }}>Case Details</Typography>
			<NextLink href="/case-details/create" passHref>
				<Button variant="contained" color="primary">Create New Case Detail</Button>
			</NextLink>
			<List>
				{caseDetails.map((detail) => (
					<ListItem key={detail.caseDetailId} divider>
						<ListItemText
							primary={`Date: ${new Date(detail.caseDetailEntryDateTime).toLocaleDateString()}`}
							secondary={
								<>
									<Typography component="span" variant="body2">
                                        Description: {detail.description}
									</Typography>
									<br />
									<Typography component="span" variant="body2">
                                        Docket Detail: {detail.docketDetail}
									</Typography>
									<br />
									{detail.documentUri && (
										<Typography component="span" variant="body2">
                                            Document: <MuiLink href={detail.documentUri}>{detail.documentUri}</MuiLink>
										</Typography>
									)}
								</>
							}
						/>
						<NextLink href={`/case-details/${detail.caseDetailId}`} passHref>
							<MuiLink>
								<Button color="primary">View</Button>
							</MuiLink>
						</NextLink>
						<IconButton onClick={() => handleDelete(detail.caseDetailId)} color="error">
							<DeleteIcon />
						</IconButton>
					</ListItem>
				))}
			</List>
		</Container>
	)
}

export const getServerSideProps = async () => {
	// Fetch data from your API
	const res = await axios.get('http://api:8080/v1/CaseDetails')
	const caseDetails = res.data // Adjust this according to the API response

	return {
		props: { caseDetails }
	}
}

CaseDetailsPage.propTypes = {
	caseDetails: PropTypes.arrayOf(
		PropTypes.shape({
			caseDetailId: PropTypes.string.isRequired,
			caseDetailEntryDateTime: PropTypes.string.isRequired,
			description: PropTypes.string,
			docketDetail: PropTypes.string,
			documentUri: PropTypes.string
		})
	).isRequired
}

export default CaseDetailsPage
